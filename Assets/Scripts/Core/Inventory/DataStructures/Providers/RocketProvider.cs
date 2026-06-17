using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct RocketProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(RocketProvider);
        public ItemType Item => ItemType.Rocket;
        public int Quantity { get; private set; }

        public RocketProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}