using Core.Equipment;
using Core.Inventory.Requests;
using Primitives.EventBus.Abstractions;

namespace Core.Inventory
{
    public interface IInventory
    {
        public IInventorySystem InventorySystem { get; }
        public IEventBus EventBus { get; }
        public IEquipment CurrentlyEquipped { get; }
        // public bool TryEquip(ItemType item);
        public int StockItem(IItemProviderRequest provider);
    }
}