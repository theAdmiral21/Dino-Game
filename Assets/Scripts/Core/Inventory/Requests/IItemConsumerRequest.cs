using Primitives.Items;

namespace Core.Inventory.Requests
{
    public interface IItemConsumerRequest
    {
        // public bool Requested { get; }
        // public Type ConsumerType { get; }
        public ItemType Item { get; }
        public int WithdrawAmount { get; }
    }
}