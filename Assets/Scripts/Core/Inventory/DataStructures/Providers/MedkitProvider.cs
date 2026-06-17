using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct MedkitProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(MedkitProvider);
        public ItemType Item => ItemType.Medkit;
        public int Quantity { get; private set; }

        public MedkitProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}