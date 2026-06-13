using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct ShellConsumer : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(ShellConsumer);
        public ItemType Item => ItemType.Shell;
        public bool Requested { get; private set; }

        public ShellConsumer(bool requested = true) => Requested = requested;
    }
}