using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise2.Enums;

namespace TrolleyApi.Exercise2.Services
{
    public interface IProductSortService
    {
        IReadOnlyList<SortOptions> SupportedSortOptions { get; }

        Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products);

    }
}
