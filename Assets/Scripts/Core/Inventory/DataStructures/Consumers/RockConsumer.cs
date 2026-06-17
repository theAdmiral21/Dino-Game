using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct RockConsumer : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(RockConsumer);
        public ItemType Item => ItemType.Rock;

        public int WithdrawAmount => _withdrawAmount;

        private int _withdrawAmount;

        public RockConsumer(int withdrawAmount) => _withdrawAmount = withdrawAmount;
    }
}