using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.FilterModels
{
    public class ProductFilter
    {
        public int? Id { get; set; }
        public string DescriptionContains { get; set; }
        public decimal? Price { get; set; }
        public int? AmountInStock { get; set; }
        public string NameContains { get; set; }
        public bool IsEmpty =>
            !Id.HasValue
            && string.IsNullOrEmpty(DescriptionContains)
            && !Price.HasValue
            && !AmountInStock.HasValue
            && string.IsNullOrEmpty(NameContains);
    }
}
