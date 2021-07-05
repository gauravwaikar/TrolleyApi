
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using Refit;

namespace TrolleyApi.Exercise2.ApiProxies
{
    public interface IProductsRepository
    {
        [Get("/products?token={token}")]
        public Task<List<Product>> Get(string token);
    }
}
