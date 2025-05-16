using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OptiWash.Models
{
    public class Organization : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        [StringLength(100)]
        public string Location { get; set; } 

        // Navigation: One organization can have many cars
        public ICollection<Car> Cars { get; set; }
    }
}
