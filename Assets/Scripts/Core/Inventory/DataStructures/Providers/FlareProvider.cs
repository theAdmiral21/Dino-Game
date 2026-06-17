using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct FlareProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(FlareProvider);
        public ItemType Item => ItemType.Flares;
        public int Quantity { get; private set; }

        public FlareProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}