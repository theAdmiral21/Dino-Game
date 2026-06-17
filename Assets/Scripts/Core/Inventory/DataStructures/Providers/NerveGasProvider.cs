using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct NerveGasProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(NerveGasProvider);
        public ItemType Item => ItemType.NerveGas;
        public int Quantity { get; private set; }

        public NerveGasProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}