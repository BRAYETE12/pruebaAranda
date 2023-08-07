using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(350);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);
            builder.Property(p => p.img).HasMaxLength(400);
            builder.Property(p => p.UserCreate).HasMaxLength(100);
            builder.Property(p => p.UserUpdate).HasMaxLength(100);
            builder.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId).IsRequired();
        }
    }
}
