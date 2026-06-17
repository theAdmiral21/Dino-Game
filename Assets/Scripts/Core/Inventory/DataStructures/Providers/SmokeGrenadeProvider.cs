using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct SmokeGrenadeProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(SmokeGrenadeProvider);
        public ItemType Item => ItemType.SmokeGrenade;
        public int Quantity { get; private set; }

        public SmokeGrenadeProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}