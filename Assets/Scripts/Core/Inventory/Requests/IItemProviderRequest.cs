using System;
using Primitives.Items;

namespace Core.Inventory.Requests
{
    public interface IItemProviderRequest
    {
        public bool Requested { get; }
        public Type ProviderType { get; }
        public ItemType Item { get; }
        public int Quantity { get; }
    }
}