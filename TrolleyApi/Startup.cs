using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrolleyApi.Excercise1;
using TrolleyApi.Exercise2.ApiProxies;
using TrolleyApi.Exercise2.Services;

namespace TrolleyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IProductSortService, PriceSortService>()
                .AddTransient<IProductSortService, NameSortService>()
                .AddTransient<IProductSortService, PopularitySortService>();

            services.AddApplicationInsightsTelemetry("793d0275-757d-41a8-a127-253b475a3fc9");
            


            services
                .AddTransient<ISortService>(sp =>
                {
                    var sortServices = sp.GetServices(typeof(IProductSortService))
                        as IEnumerable<IProductSortService>;
                    var productRepository = sp.GetRequiredService<IProductsRepository>();
                    return new SortService(Configuration, productRepository, sortServices.ToList());
                });

            var resourceApiBaseUrl = new Uri(Configuration["ResourceApiBaseUrl"]);

            services
                .AddHttpClient("ProductsRepository", c =>
                {
                    c.BaseAddress = resourceApiBaseUrl;
                })
                .AddTypedClient(c => Refit.RestService.For<IProductsRepository>(c));

            services
                .AddHttpClient("ShopperHistoryRepository", c =>
                {
                    c.BaseAddress = resourceApiBaseUrl;
                })
                .AddTypedClient(c => Refit.RestService.For<IShoppingHistoryRepository>(c));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
