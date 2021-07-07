using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise3.Domain;

namespace TrolleyApi.Exercise3.Services
{
    public interface ITrolleyTotalService
    {
        decimal Calculate(CalculateTrolleyTotalRequest request);
    }
    public class TrolleyTotalService : ITrolleyTotalService
    {
        private readonly INormalPriceProcessorService _normalPriceProcessorService;
        private readonly ISpecialPriceProcessorService _specialPriceProcessorService;

        public TrolleyTotalService(INormalPriceProcessorService normalPriceProcessorService, ISpecialPriceProcessorService specialPriceProcessorService)
        {
            _normalPriceProcessorService = normalPriceProcessorService;
            _specialPriceProcessorService = specialPriceProcessorService;
        }
        public decimal Calculate(CalculateTrolleyTotalRequest request)
        {
            var trolleyTotal = 0m;

            //Get the specials total
            if(request.Specials != null)
            {
                foreach (var eachSpecial in request.Specials)
                {
                    trolleyTotal += _specialPriceProcessorService.Calculate(eachSpecial, request.Quantities);
                }
            }


            foreach (var eachPurchasedQuantity in request.Quantities)
            {
                trolleyTotal += _normalPriceProcessorService.Calculate(
                    eachPurchasedQuantity,
                    request.Products);
            }

            return trolleyTotal;
        }
    }
}
