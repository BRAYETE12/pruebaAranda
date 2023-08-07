using Domain.Models;
using Domain.Models.TablasReferencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(350);

            var random = new Random();

            var Categories =
                Enumerable.Range(1, 4)
                .Select(s => new Category {
                    Id = s,
                    Name = $"Category {s}",
                    UserCreate = "-",
                    UserUpdate = "-",
                });

            builder.HasData(Categories);

        }
    }
}
