using DronesTech.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace DronesTech.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_-]*$", ErrorMessage = "The name must contain only letters, numbers, dash and underscore.@@")]
        public string Name { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        [RegularExpression("^[A-Z0-9_]*$", ErrorMessage = "The name must contain only capital letters, numbers and underscore.")]
        public string Code { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
