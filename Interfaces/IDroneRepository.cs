using DronesTech.Models;

namespace DronesTech.Interfaces
{
    public interface IDroneRepository
    {
        ICollection<Drone> GetDrones();
    }
}
