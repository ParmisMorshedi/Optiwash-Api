using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WashRecord
{
    [Key]
    public int Id { get; set; }


  

    [ForeignKey("CarId")]
    public Car Car { get; set; }
    public int CarId { get; set; }


    [SwaggerSchema(Format = "uuid", Description = "User ID in GUID format")]

    [ForeignKey("UserId")]
    public User User { get; set; }
    public string UserId { get; set; }
    [Required]
    public DateTime WashDate { get; set; } 

    public bool InteriorCleaned { get; set; } = false;

    public bool ExteriorCleaned { get; set; } = false; 

    [StringLength(500)]
    public string Notes { get; set; } 
}
