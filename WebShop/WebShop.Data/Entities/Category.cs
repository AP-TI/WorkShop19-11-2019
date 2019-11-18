using System.Collections.Generic;

namespace WebShop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
