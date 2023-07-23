﻿using DronesTech.Context;
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

        public bool DroneExists(int id)
        {
            var drone = _context.Drones.Where(d => d.Id == id).FirstOrDefault();
            return drone != null ? true : false;
        }

        public Drone GetDroneById(int id)
        {
            return _context.Drones.Where(d => d.Id == id).FirstOrDefault();
        }

        public int GetDroneBattery(int id)
        {
            return _context.Drones.Find(id).BatteryCapacity;
        }

        public bool IsDroneEmpty(int id)
        {
            var drone = GetDroneById(id);
            return drone != null && drone.Medicines.Count == 0 ? true : false;
        }

        public Drone ChargeMedicines(Drone drone, ICollection<Medicine> medicines)
        {
            drone.Medicines = medicines;
            _context.SaveChanges();
            return drone;
        }
    }
}
