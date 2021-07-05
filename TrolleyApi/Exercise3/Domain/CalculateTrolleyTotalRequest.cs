using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Domain;

namespace TrolleyApi.Exercise3.Domain
{
    public class CalculateTrolleyTotalRequest
    {
        public List<Product> Products { get; set; }

        public List<Special> Specials { get; set; }

        public List<PurchasedQuantity> Quantities { get; set; }
    }
}
