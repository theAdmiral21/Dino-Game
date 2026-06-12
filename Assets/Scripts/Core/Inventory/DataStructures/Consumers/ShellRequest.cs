using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct ShellRequest : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(ShellRequest);
        public ItemType Item => ItemType.Shell;
        public bool Requested { get; private set; }

        public ShellRequest(bool requested = true) => Requested = requested;
    }
}