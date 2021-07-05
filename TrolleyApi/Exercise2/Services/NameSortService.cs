using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise2.Enums;

namespace TrolleyApi.Exercise2.Services
{
    public class NameSortService : IProductSortService
    {
        public IReadOnlyList<SortOptions> SupportedSortOptions => new List<SortOptions> { SortOptions.ASCENDING, SortOptions.DESCENDING };

        public Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products)
        {
            var sorted = option == SortOptions.DESCENDING ?
                 products.OrderByDescending(p => p.Name) :
                 products.OrderBy(p => p.Name);

            return Task.FromResult<IReadOnlyList<Product>>(sorted.ToList());
        }
    }
}
