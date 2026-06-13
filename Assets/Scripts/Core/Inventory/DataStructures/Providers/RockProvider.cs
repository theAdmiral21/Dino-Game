using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct RockProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(RockProvider);
        public ItemType Item => ItemType.Rock;
        public bool Requested { get; private set; }
        public int Quantity { get; private set; }

        public RockProvider(int quantity, bool requested = true)
        {
            Quantity = quantity;
            Requested = requested;
        }
    }
}