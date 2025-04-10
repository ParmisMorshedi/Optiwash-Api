namespace OptiWash.Models.DTOs
{
    public class WashCarDto
    {
        public string PlateNumber { get; set; }
        public bool Interior { get; set; }
        public bool Exterior { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
    }
}
