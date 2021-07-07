using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;
using TrolleyApi.Exercise3.Domain;

namespace TrolleyApi.Exercise3.Services
{
    public interface INormalPriceProcessorService
    {
        decimal Calculate(PurchasedQuantity purchasedQuantity, List<Product> products);
    }
    public class NormalPriceProcessorService : INormalPriceProcessorService
    {
        public decimal Calculate(PurchasedQuantity purchasedQuantity, List<Product> products)
        {
            if (purchasedQuantity.QuantityRemainingToBeBilled <= 0)
                return 0m;

            var cost = products
                .Where(p => p.Name == purchasedQuantity.Name)
                .Select(p => p.Price)
                .FirstOrDefault();

            return Convert.ToDecimal(purchasedQuantity.QuantityRemainingToBeBilled) * cost;
        }
    }
}
