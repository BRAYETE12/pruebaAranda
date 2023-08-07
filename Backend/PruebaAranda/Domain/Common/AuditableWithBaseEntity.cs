namespace Domain.Common
{
    public class AuditableWithBaseEntity<T> : BaseEntity<T>, IAuditableEntity
    {
        public string UserCreate { get; set; } = string.Empty;
        public string UserUpdate { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Estado { get; set; }
    }
}
