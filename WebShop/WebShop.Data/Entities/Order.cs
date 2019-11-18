using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }

        public virtual List<Product> OrderedProducts { get; set; }
    }
}
