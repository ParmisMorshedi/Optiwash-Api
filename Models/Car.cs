using OptiWash.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Car : AuditableEntity
{
    [Key] 
    public int Id { get; set; }
    public int? OrganizationId { get; set; }

    [Required]
    public string PlateNumber { get; set; }
    public string? ScannedLicensePlate { get; set; }

    [ForeignKey("OrganizationId")]
    public Organization? Organization { get; set; }
    public ICollection<WashRecord> WashHistory { get; set; }
}
