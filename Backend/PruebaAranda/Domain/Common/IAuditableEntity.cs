namespace Domain.Common
{
    public interface IAuditableEntity
    {
        string UserCreate { get; set; }
        string UserUpdate { get; set; }
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        bool Estado { get; set; }
    }
}
