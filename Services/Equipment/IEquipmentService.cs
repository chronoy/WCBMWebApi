using Models;

namespace Services
{
    public interface IEquipmentService
    {
        public Task<List<Equipment>> GetEquipments(string? company, string? line, string? station, string? category, string? model, string? manufacturer);

        public Task<List<string>> GetEquipmentSerialNumbers();

        public Task<List<Equipment>> GetEquipmentInfo(List<string> serialNumbers);

        public Task<bool> ValidSerialNumber(string serialNumber);

        public Task<string> AddEquipment(Equipment entity);

        public Task<string> AddEquipments(List<Equipment> entities);

        public Task<string> UpdateEquipment(Equipment entity);

        public Task<bool> DeleteEquipment(int id);
    }
}