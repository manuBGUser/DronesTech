using DronesTech.Models.Types;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DronesTech.DTO
{
    public class DroneDTO
    {
        public string SerieNumber { get; set; }
        public int Model { get; set; }
        public Decimal WeightLimit { get; set; }
        public int BatteryCapacity { get; set; }
        public int Status { get; set; }
    }
}
