using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.Exercise2.Enums;

namespace TrolleyApi.Exercise2.Services
{
    public class SortOptionValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var parsedValue = value as string;            
            return Enum.IsDefined(typeof(SortOptions), parsedValue?.Trim()?.ToUpperInvariant());
        }
    }
}
