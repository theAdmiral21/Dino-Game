using System.Collections.Generic;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items { get; }
        public void AddItem(IItemProviderRequest provider);
        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer);
    }
}