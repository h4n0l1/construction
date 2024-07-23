using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class ConstructionCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Location { get; set; }

        [Required]
        public int Stage { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Details { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }
    }
}
