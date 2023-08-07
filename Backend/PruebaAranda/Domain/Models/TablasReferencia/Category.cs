using Domain.Common;

namespace Domain.Models.TablasReferencia
{
    public class Category : AuditableWithBaseEntity<int>
    {
        public string Name { get; set; }
    }
}
