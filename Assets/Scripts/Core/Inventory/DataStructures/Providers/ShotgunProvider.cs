using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct ShotgunProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(ShotgunProvider);
        public ItemType Item => ItemType.Shotgun;
        public bool Requested { get; private set; }
        public int Quantity { get; private set; }

        public ShotgunProvider(int quantity, bool requested = true)
        {
            Quantity = quantity;
            Requested = requested;
        }
    }
}