using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SSD_Lab1.Models
{
    // Extends IdentityUser. Do NOT re-declare UserName/Email/PhoneNumber.
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;

        [Required, StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = default!;

        [StringLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }
    }
}
