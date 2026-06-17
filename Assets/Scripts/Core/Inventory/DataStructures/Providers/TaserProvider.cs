using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct TaserProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(TaserProvider);
        public ItemType Item => ItemType.Taser;
        public int Quantity { get; private set; }

        public TaserProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}