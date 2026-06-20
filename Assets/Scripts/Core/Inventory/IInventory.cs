using System.Collections.Generic;
using Core.Equipment;
using Core.Inventory.Requests;
using Primitives.EventBus.Abstractions;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventory
    {
        public IInventorySystem InventorySystem { get; }
        public IEventBus InventoryEventBus { get; }
        public IEquipment CurrentlyEquipped { get; }
        // public bool TryEquip(ItemType item);
        public int StockItem(IItemProviderRequest provider);
    }
}