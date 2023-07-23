using DronesTech.Models;

namespace DronesTech.Interfaces
{
    public interface IDroneRepository
    {
        ICollection<Drone> GetDrones();
        ICollection<Drone> GetAbleDrones();
        bool CreateDrone(Drone drone);

        int DroneBatery(Guid id);

    }
}
