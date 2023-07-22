using DronesTech.Models.Types;
using System.ComponentModel.DataAnnotations;

namespace DronesTech.Models
{
    public class Drone
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string SerieNumber { get; set; }
        [Required]
        public ModelType Model { get; set; }
        [Required]
        [Range(0,500)]
        public Decimal WeightLimit { get; set; }
        [Required]
        [Range(0, 100)]
        public int BatteryCapacity { get; set; }
        [Required]
        public StatusType Status { get; set; }
    }
}
