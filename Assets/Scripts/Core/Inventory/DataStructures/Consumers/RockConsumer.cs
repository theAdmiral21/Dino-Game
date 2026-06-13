using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct RockConsumer : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(RockConsumer);
        public ItemType Item => ItemType.Rock;
        public bool Requested { get; private set; }

        public RockConsumer(bool requested = true) => Requested = requested;
    }
}