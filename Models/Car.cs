using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Car
{
    [Key] 
    public int Id { get; set; }

    [Required]
    [StringLength(10, MinimumLength = 6)]
    public string PlateNumber { get; set; }
    public string? ScannedLicensePlate { get; set; }
    public ICollection<WashRecord> WashHistory { get; set; }
}
