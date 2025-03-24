using System.ComponentModel.DataAnnotations;

namespace OptiWash.Models.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }
        public string? ScannedLicensePlate { get; set; }
    }
}
