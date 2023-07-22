using DronesTech.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRest.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
