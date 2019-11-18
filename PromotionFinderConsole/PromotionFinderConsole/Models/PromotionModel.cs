namespace PromotionFinderConsole.Models
{
    public class PromotionModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
