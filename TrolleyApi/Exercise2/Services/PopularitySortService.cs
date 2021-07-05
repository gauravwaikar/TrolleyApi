using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise2.Enums;

namespace TrolleyApi.Exercise2.Services
{
    public class PopularitySortService : IProductSortService
    {
        private readonly IShoppingHistoryRepository _shopperHistoryRepository;
        private readonly IConfiguration _configuration;

        public PopularitySortService(
            IShoppingHistoryRepository shopperHistoryRepository,
            IConfiguration configuration)
        {
            _shopperHistoryRepository = shopperHistoryRepository;
            _configuration = configuration;
        }

        public IReadOnlyList<SortOptions> SupportedSortOptions =>
            new List<SortOptions> { SortOptions.RECOMMENDED };

        public async Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products)
        {
            var shopperHistory = await _shopperHistoryRepository.Get(_configuration["UserToken"]);

            var productIdAndPurchaseCountMap = GetProductIdPurchaseCountMap(shopperHistory);

            return (from eachProduct in products
                    join eachPurchaseHisotry in productIdAndPurchaseCountMap
                     on eachProduct.Name equals eachPurchaseHisotry.Item1
                     into gj
                    from subPurchaseHistory in gj.DefaultIfEmpty()
                    select new
                    {
                        Product = eachProduct,
                        PurchaseCount = subPurchaseHistory == null ? 0d : subPurchaseHistory.Item2
                    })
                   .OrderByDescending(r => r.PurchaseCount)
                   .Select(r => r.Product)
                   .ToList();
        }

        private static List<Tuple<string, double>> GetProductIdPurchaseCountMap(List<ShopperHistoryResponse> shopperHistory)
        {
            return shopperHistory
                .SelectMany(sp => sp.Products)
                .GroupBy(p => p.Name)
                .Select(grouped => new Tuple<string, double>(grouped.Key, grouped.Sum(g => g.Quantity)))
                .ToList();
        }
    }
}
