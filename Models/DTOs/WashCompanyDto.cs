namespace OptiWash.Models.DTOs
{
    public class WashCompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<WashCarDto> Cars { get; set; }
    }
}
