using System.Collections.Generic;

namespace TrolleyApi.Exercise3.Domain
{
    public class Special
    {
        public List<SpecialQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}