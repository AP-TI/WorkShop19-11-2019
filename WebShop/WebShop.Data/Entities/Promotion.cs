namespace WebShop.Data.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
