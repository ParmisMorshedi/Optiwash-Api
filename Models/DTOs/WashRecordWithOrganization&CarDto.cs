using OptiWash.Models.Enums;

namespace OptiWash.Models.DTOs
{
    public class WashRecordWithOrganization_CarDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarPlateNumber { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationCity { get; set; }
        public DateTime WashDate { get; set; }
        public bool InteriorCleaned { get; set; }
        public bool ExteriorCleaned { get; set; }
        public WashStatus Status { get; set; }
        public string? Notes { get; set; }
    }
}
