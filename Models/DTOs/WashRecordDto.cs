using System.ComponentModel.DataAnnotations;

namespace OptiWash.Models.DTOs
{
    public class WashRecordDto
    {
        [Required]

        public int Id { get; set; }
        [Required]

        public int CarId { get; set; }
        public string UserId { get; set; }
        public DateTime WashDate { get; set; }

        public bool InteriorCleaned { get; set; } = false;

        public bool ExteriorCleaned { get; set; } = false;

 
        public string Notes { get; set; }
    }
}
