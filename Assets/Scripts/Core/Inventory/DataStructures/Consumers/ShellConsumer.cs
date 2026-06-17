using System;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory.DataStructures.Consumers
{
    public struct ShellConsumer : IItemConsumerRequest
    {
        public Type ConsumerType => typeof(ShellConsumer);
        public ItemType Item => ItemType.Shell;
        public int WithdrawAmount => _withdrawAmount;

        private int _withdrawAmount;

        public ShellConsumer(int withdrawAmount) => _withdrawAmount = withdrawAmount;
    }
}