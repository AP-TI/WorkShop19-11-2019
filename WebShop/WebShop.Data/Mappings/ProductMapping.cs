using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebShop.Data.Entities;

namespace WebShop.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Price);
            builder.Property(x => x.AmountInStock);
            builder.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(x => x.CategoryId).IsRequired();

            //TODO indexes 1: index product names. Also make sure these names are unique throughout our data
            //After adding your index, you need to create a migration and update the database:
            //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs

            //TODO indexes 2: index the combination of amountinstock and name
            //After adding your index, you need to create a migration and update the database:
            //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs

        }
    }
}
