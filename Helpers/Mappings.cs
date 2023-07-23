using AutoMapper;
using DronesTech.DTO;
using DronesTech.Models;

namespace DronesTech.Helpers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Drone, DroneDTO>();
            CreateMap<DroneDTO, Drone>();
            CreateMap<Medicine, MedicineDTO>();
            CreateMap<MedicineDTO, Medicine>();
        }
    }
}
