using Core.Equipment;
using Core.Inventory;
using Core.Inventory.Requests;
using Primitives.EventBus.Abstractions;
using Primitives.Items;

namespace Application.Inventory.InventoryItems
{
    public class RockInventory : BaseInventoryItem
    {
        public RockInventory(ItemType item, int maxAllowed, IEventBus inventoryEventBus, IEquipment equipment) : base(item, maxAllowed, inventoryEventBus, equipment)
        {
        }
    }
}