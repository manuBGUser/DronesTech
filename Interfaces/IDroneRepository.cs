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
        Drone GetDroneBySerieNumber(string serieNumber);
        int GetDroneBattery(int id);
        bool IsDroneEmpty(int id);
        bool IsDroneAbled(int id);
        Drone ChargeMedicines(Drone drone, ICollection<Medicine> medicines);
    }
}
