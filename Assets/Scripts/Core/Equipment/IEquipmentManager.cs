using Core.Inventory;
using Movement.Core.Movement.DataStructures;
using Primitives.EventBus.Abstractions;

namespace Core.Equipment
{
    public interface IEquipmentManager
    {
        public IEquipment ActiveEquipment { get; }
        public void HandleEquipmentChanged(CurrentEquipmentChanged evt);
        public void HandleEquipmentChangeResult(SwitchEquipmentResult result);
        public void HandleEquipmentIndexResult(IndexEquipmentResult result);
        public void Init(IEventBus inventoryEventBus, IInventorySystem inventorySystem);
    }
}
