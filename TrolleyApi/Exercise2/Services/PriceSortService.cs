using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise2.Enums;

namespace TrolleyApi.Exercise2.Services
{
    public class PriceSortService : IProductSortService
    {
        public IReadOnlyList<SortOptions> SupportedSortOptions =>
               new List<SortOptions> { SortOptions.HIGH, SortOptions.LOW };

        public Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products)
        {
            var sorted = option == SortOptions.HIGH ?
                 products.OrderByDescending(p => p.Price) :
                 products.OrderBy(p => p.Price);

            return Task.FromResult<IReadOnlyList<Product>>(sorted.ToList());
        }
    }
}
