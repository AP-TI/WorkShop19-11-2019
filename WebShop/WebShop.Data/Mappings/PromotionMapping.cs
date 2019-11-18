using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebShop.Data.Entities;

namespace WebShop.Data.Mappings
{
    public class PromotionMapping : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.CategoryId);
            builder.HasOne(x => x.Category).WithOne(c => c.Promotion).HasForeignKey<Promotion>(x => x.CategoryId);
            builder.Property(x => x.DiscountPercentage);
        }
    }
}
