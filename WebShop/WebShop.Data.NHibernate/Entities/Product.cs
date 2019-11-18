namespace WebShop.Data.NHibernate.Entities
{
    public class Product
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int AmountInStock { get; set; }
    }
}
