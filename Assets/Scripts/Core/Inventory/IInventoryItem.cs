using Core.Equipment;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventoryItem
    {
        public ItemType Item { get; }
        public IEquipment Equipment { get; }
        public int Quantity { get; }
        public int Deposit(int amount);
        public int Withdraw(int amount);
    }
}