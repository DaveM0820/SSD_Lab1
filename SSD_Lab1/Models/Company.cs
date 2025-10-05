using System.ComponentModel.DataAnnotations;

namespace SSD_Lab1.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Company Name")]
        public string Name { get; set; } = default!;

        [Required]
        [Range(0, 200, ErrorMessage = "Years must be between {1} and {2}.")]
        [Display(Name = "Years in Business")]
        public int YearsInBusiness { get; set; }

        [Required, Url]
        [Display(Name = "Website URL")]
        public string Website { get; set; } = default!;

        [StringLength(50)]
        public string? Province { get; set; }
    }
}
