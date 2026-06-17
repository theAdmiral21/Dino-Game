using Core.Equipment;
using Core.Inventory.Requests;
using Primitives.EventBus.Abstractions;
using Primitives.Items;

namespace Application.Inventory
{
    public class InventoryItem : BaseInventoryItem
    {
        public InventoryItem(ItemType item, int maxAllowed, IEventBus inventoryEventBus, IEquipment equipment) : base(item, maxAllowed, inventoryEventBus, equipment) { }

    }
}