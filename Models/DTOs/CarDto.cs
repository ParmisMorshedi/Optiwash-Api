using System.ComponentModel.DataAnnotations;

namespace OptiWash.Models.DTOs
{
    public class CarDto
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Za-z]{3}[0-9]{3}$", ErrorMessage = "Ogiltigt registreringsnummer.")]
        public string PlateNumber { get; set; }

        [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Ogiltigt skannat registreringsnummer.")]
        public string? ScannedLicensePlate { get; set; }
    }
}
