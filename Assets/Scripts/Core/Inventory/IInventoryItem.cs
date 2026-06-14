using Core.Equipment;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Core.Inventory
{
    public interface IInventoryItem
    {
        public ItemType Item { get; }
        public int Quantity { get; }
        public bool PreviouslyFound { get; }
        public void AddItem(IItemProviderRequest provider);
        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer);
        public bool CanAdd(IItemProviderRequest provider);
        public bool CanConsume(IItemConsumerRequest consumer);
        // public void EquipItem();
        public IEquipment GetEquipment();
    }
}