using System;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventoryItem
    {
        public ItemType Item { get; }
        public int Quantity { get; }
        public int Deposit(int amount);
        public int Withdraw(int amount);
        public void HandleFire(int amount);
        public void HandleReload(int requestedAmount, Action<int> replenishCallback);
    }
}