using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct CanisterProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(CanisterProvider);
        public ItemType Item => ItemType.Canister;
        public int Quantity { get; private set; }

        public CanisterProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}