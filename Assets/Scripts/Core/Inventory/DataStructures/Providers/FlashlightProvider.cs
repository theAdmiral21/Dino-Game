using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct FlashlightProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(FlashlightProvider);
        public ItemType Item => ItemType.Flashlight;
        public int Quantity { get; private set; }

        public FlashlightProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}