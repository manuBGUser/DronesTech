using DronesTech.Context;
using DronesTech.Interfaces;
using DronesTech.Models;
using DronesTech.Models.Types;

namespace DronesTech.Repository
{
    public class DroneRepository : IDroneRepository
    {
        private readonly ApplicationDbContext _context;
        public DroneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Drone> GetDrones()
        {
            return _context.Drones.ToList();
        }

        public ICollection<Drone> GetAbleDrones()
        {
            return _context.Drones.Where(d => d.BatteryCapacity >= 25 && (d.Status == StatusType.Inactive || d.Status == StatusType.Charged)).ToList();
        }

        public bool CreateDrone(Drone drone)
        {
            _context.Add(drone);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
