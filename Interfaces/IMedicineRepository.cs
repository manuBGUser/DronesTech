using DronesTech.Models;

namespace DronesTech.Interfaces
{
    public interface IMedicineRepository
    {
        ICollection<Medicine> GetMedicines();
        ICollection<Medicine> GetMedicinesToCharge(decimal weightLimit);
        bool CreateMedicine(Medicine medicine);
        ICollection<Medicine> GetMedicinesByIds(List<int> ids);
        decimal GetMedicinesWeights(ICollection<Medicine> medicines);
    }
}
