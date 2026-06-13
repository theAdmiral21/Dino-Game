using System.Collections.Generic;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items { get; }
        public ItemType CurrentlyEquipped { get; }
        public bool AddItem(IItemProviderRequest provider);
        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer);
        public bool TryEquip(ItemType item);
    }
}