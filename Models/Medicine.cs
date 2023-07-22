using DronesTech.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace DronesTech.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
