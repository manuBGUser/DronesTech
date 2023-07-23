using DronesTech.Context;
using DronesTech.Interfaces;
using DronesTech.Models;
using DronesTech.Models.Types;
using System.Text;

namespace DronesTech.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateMedicine(Medicine medicine)
        {
            _context.Add(medicine);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<Medicine> GetMedicines()
        {
            return _context.Medicines.ToList();
        }

        public ICollection<Medicine> GetMedicinesToCharge(decimal weightLimit)
        {
            var lowerThan = _context.Medicines.Where(m => m.Weight <= weightLimit).ToList();
            List<Medicine> medicines = new List<Medicine>();
            foreach (var m in lowerThan)
            {
                if (medicines.Sum(m => m.Weight) + m.Weight <= weightLimit)
                    medicines.Add(m);
                if (medicines.Sum(m => m.Weight) == weightLimit)
                    break;
            }
            return medicines;
        }
    }
}
