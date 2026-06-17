using System.Collections.Generic;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items { get; }
        public IInventoryItem CurrentlyEquipped { get; }
        public int RestockItem(IItemProviderRequest provider);
        // public bool AddNewItem(IItemProviderRequest provider);
        public int ConsumeItem(IItemConsumerRequest consumer);
        public bool TryEquip(ItemType item);
    }
}