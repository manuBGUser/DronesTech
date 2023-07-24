using DronesTech.Models.Types;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Range(0.00, 500.00)]
        [Column(TypeName = "decimal(5,2)")]
        public Decimal WeightLimit { get; set; }
        [Required]
        [Range(0, 100)]
        public int BatteryCapacity { get; set; }
        [Required]
        public StatusType Status { get; set; }

        public ICollection<Medicine> Medicines { get; set; }
    }
}
