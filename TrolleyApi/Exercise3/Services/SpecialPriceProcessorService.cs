using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise3.Domain;

namespace TrolleyApi.Exercise3.Services
{
    public interface ISpecialPriceProcessorService
    {
        decimal Calculate(Special special, List<PurchasedQuantity> purchasedQuantities);
    }
    public class SpecialPriceProcessorService : ISpecialPriceProcessorService
    {
        public decimal Calculate(Special special, List<PurchasedQuantity> purchasedQuantities)
        {
            if (!AreMatchingSpecialProductsNotBilled(special, purchasedQuantities))
                return 0;

            ApplySpecial(special, purchasedQuantities);

            return special.Total;
        }

        private static bool AreMatchingSpecialProductsNotBilled(
            Special special,
            List<PurchasedQuantity> purchasedQuantities)
        {
            foreach (var eachProductInSpecialGroup in special.Quantities)
            {
                var matchingProductInTrollery = purchasedQuantities
                    .FirstOrDefault(pq => pq.Name == eachProductInSpecialGroup.Name &&
                                  eachProductInSpecialGroup.Quantity > 0 &&
                                  pq.QuantityRemainingToBeBilled >= eachProductInSpecialGroup.Quantity);

                if (matchingProductInTrollery == null)
                    return false;
            }

            return true;
        }

        private void ApplySpecial(
            Special special,
            List<PurchasedQuantity> purchasedQuantities)
        {
            foreach (var eachProductInSpecialGroup in special.Quantities)
            {
                var matchingProductInTrollery = purchasedQuantities
                    .FirstOrDefault(pq => pq.Name == eachProductInSpecialGroup.Name);

                matchingProductInTrollery.MarkQuantityAsBilled(eachProductInSpecialGroup.Quantity);
            }

        }
    }
}
