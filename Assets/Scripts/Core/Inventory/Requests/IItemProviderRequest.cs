using System;
using Primitives.Items;

namespace Core.Inventory.Requests
{
    public interface IItemProviderRequest
    {
        public int Quantity { get; }
        public ItemType Item { get; }
    }
}