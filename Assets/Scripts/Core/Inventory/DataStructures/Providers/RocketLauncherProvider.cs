using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Providers
{
    public struct RocketLauncherProvider : IItemProviderRequest
    {
        public Type ProviderType => typeof(RocketLauncherProvider);
        public ItemType Item => ItemType.RocketLauncher;
        public int Quantity { get; private set; }

        public RocketLauncherProvider(int quantity)
        {
            Quantity = quantity;
        }
    }
}