using Core.Inventory;

namespace Core.Equipment
{
    public interface IEquipmentManager
    {
        public IEquipment ActiveEquipment { get; }
        public void SwitchEquipment(CurrentEquipmentChanged evt);
    }
}
