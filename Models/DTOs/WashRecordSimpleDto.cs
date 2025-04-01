using OptiWash.Models.Enums;

namespace OptiWash.Models.DTOs
{
    public class WashRecordSimpleDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string? CarPlateNumber { get; set; } 
        public DateTime WashDate { get; set; }
        public bool InteriorCleaned { get; set; }
        public bool ExteriorCleaned { get; set; }
        public string? Notes { get; set; }
        public WashStatus Status { get; set; }
    }
}
