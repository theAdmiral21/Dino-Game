using Core.Inventory;
using Primitives.EventBus.Abstractions;

namespace Core.Equipment
{
    public interface IEquipmentManager
    {
        public IEquipment ActiveEquipment { get; }
        public void HandleEquipmentChanged(CurrentEquipmentChanged evt);
        public void Init(IEventBus inventoryEventBus, IInventorySystem inventorySystem);
    }
}
