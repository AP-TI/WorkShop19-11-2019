using WebShop.Data.Entities;

namespace WebShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }
        public string Category { get; set; }

        public ProductModel(Product product)
        {
            Id = product.Id;
            CategoryId = product.CategoryId;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            AmountInStock = product.AmountInStock;
            Category = product.Category?.Description;
        }
    }
}
