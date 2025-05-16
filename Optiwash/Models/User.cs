using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; }
    public bool IsAdmin { get; set; } 
    public List<WashRecord> WashRecords { get; set; } = new List<WashRecord>();
}
