using DronesTech.Models.Types;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DronesTech.DTO
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal Weight { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
    }
}
