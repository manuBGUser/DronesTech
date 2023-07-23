using DronesTech.Models;

namespace DronesTech.Interfaces
{
    public interface IMedicineRepository
    {
        ICollection<Medicine> GetMedicines();
        ICollection<Medicine> GetMedicinesToCharge(decimal weightLimit);
        bool CreateMedicine(Medicine medicine);
    }
}
