using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct ShotgunConsumer : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(ShotgunConsumer);
        public ItemType Item => ItemType.Shotgun;

        public int WithdrawAmount => _withdrawAmount;
        private int _withdrawAmount;
        public ShotgunConsumer(int withdrawAmount) => _withdrawAmount = withdrawAmount;
    }
}