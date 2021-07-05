using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrolleyApi.Exercise2.Domain
{
    public class ShopperHistoryResponse
    {
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
