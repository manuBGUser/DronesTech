using DronesTech.Context;
using DronesTech.Interfaces;
using DronesTech.Models;

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
    }
}
