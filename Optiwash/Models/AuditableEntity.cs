namespace OptiWash.Models
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

}
