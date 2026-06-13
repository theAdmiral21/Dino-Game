using Core.Inventory.Requests;
using Primitives.EventBus.Abstractions;
using Primitives.Items;

namespace Application.Inventory
{
    public class InventoryItem : BaseInventoryItem
    {
        public InventoryItem(ItemType item, int maxAllowed, IEventBus inventoryEventBus) : base(item, maxAllowed, inventoryEventBus) { }

        public override bool CanAdd(IItemProviderRequest provider)
        {
            if (Quantity >= _maxAllowed) return false;

            if (provider.Item != Item) return false;
            // Both of these will need to check if the base item is available when attempting to add/consume ammo.

            return true;
        }

        public override bool CanConsume(IItemConsumerRequest consumer)
        {
            if (Quantity <= 0) return false;

            // Both of these will need to check if the base item is available when attempting to add/consume ammo.

            return true;
        }
    }
}