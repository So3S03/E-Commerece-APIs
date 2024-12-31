namespace Karim.ECommerce.Domain.Contracts._Common
{
    public interface IBaseAuditableEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
