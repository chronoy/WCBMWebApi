using Models;

namespace Respository
{
    public interface IEquipmentRespository
    {
        public List<Equipment> GetEquipments(string? companyId, string? lineId, string? stationId, string? categoryId, string? modelId, string? manufacturerId);

        public List<string> GetEquipmentSerialNumbers();

        public List<Equipment> GetEquipmentInfo(List<string> serialNumbers);

        public bool ValidSerialNumber(string serialNumber);

        public string AddEquipment(Equipment entity);

        public string AddEquipments(List<Equipment> entities);

        public string UpdateEquipment(Equipment entity);

        public bool UpdateEquipments(List<Equipment> listEntity);

        public bool DeleteEquipment(int id);
    }
}