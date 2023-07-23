using DronesTech.Models;

namespace DronesTech.Interfaces
{
    public interface IDroneRepository
    {
        ICollection<Drone> GetDrones();
        ICollection<Drone> GetAbleDrones();
        bool CreateDrone(Drone drone);
        bool DroneExists(int id);
        Drone GetDroneById(int id);
        int GetDroneBatery(int id);

    }
}
