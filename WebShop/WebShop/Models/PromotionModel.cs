using WebShop.Data.Entities;

namespace WebShop.Models
{
    public class PromotionModel
    {

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal DiscountPercentage { get; set; }

        public PromotionModel(Promotion promotion)
        {
            Id = promotion.Id;
            CategoryId = promotion.CategoryId;
            Name = promotion.Name;
            Category = promotion.Category.Description;
            DiscountPercentage = promotion.DiscountPercentage;
        }
    }
}
