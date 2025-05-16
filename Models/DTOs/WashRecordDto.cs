using OptiWash.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OptiWash.Models.DTOs
{
    //using this for create/update
    public class WashRecordDto
    {    
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }
        //public string UserId { get; set; }
        public DateTime WashDate { get; set; }

        public bool InteriorCleaned { get; set; } = false;

        public bool ExteriorCleaned { get; set; } = false;

        public WashStatus Status { get; set; } = WashStatus.Pending;
        public string Notes { get; set; }
    }
}
