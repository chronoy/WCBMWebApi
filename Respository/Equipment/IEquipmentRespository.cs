using Models;

namespace Respository
{
    public interface IEquipmentRespository
    {
        public List<Equipment> GetEquipments(string? company, string? line, string? station, string? category, string? model, string? manufacturer);

        public string AddEquipment(Equipment entity);

        public string AddEquipments(List<Equipment> entities);

        public string UpdateEquipment(Equipment entity);

        public bool UpdateEquipments(List<Equipment> listEntity);

        public bool DeleteEquipment(int id);
    }
}