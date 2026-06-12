using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct ShellProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(ShellProvider);
        public ItemType Item => ItemType.Shell;
        public bool Requested { get; private set; }
        public int Quantity { get; private set; }

        public ShellProvider(int quantity, bool requested = true)
        {
            Quantity = quantity;
            Requested = requested;
        }
    }
}