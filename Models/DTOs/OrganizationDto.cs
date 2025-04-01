namespace OptiWash.Models.DTOs
{
    public class OrganizationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<string>? CarPlateNumbers { get; set; }

    }
}
