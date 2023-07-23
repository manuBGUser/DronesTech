using DronesTech.Models;

namespace DronesTech.Interfaces
{
    public interface IDroneRepository
    {
        ICollection<Drone> GetDrones();
        ICollection<Drone> GetAbledDrones();
        bool CreateDrone(Drone drone);
        bool DroneExists(int id);
        Drone GetDroneById(int id);
        int GetDroneBattery(int id);
        bool IsDroneEmpty(int id);
        Drone ChargeMedicines(Drone drone, ICollection<Medicine> medicines);
    }
}
