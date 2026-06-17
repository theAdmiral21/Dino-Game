using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct RockProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(RockProvider);
        public ItemType Item => ItemType.Rock;
        public int Quantity { get; private set; }

        public RockProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}