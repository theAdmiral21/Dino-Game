using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct ShotgunConsumer : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(ShotgunConsumer);
        public ItemType Item => ItemType.Shotgun;
        public bool Requested { get; private set; }

        public ShotgunConsumer(bool requested = true) => Requested = requested;
    }
}