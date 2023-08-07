using Domain.Common;
using Domain.Models.TablasReferencia;

namespace Domain.Models
{
    public class Product : AuditableWithBaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int CategoryId { get; set; }
        public string? img { get; set; } = default!;

        public Category Category { get; set; }
    }
}
