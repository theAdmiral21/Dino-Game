using Primitives.Items;
using Core.Inventory.Requests;

namespace Core.Equipment
{
    public interface IMagazine
    {
        public ItemType AmmoType { get; }
        public int RoundCount { get; }
        public int Capacity { get; }
        public bool CanAdd(IItemProviderRequest provider);
        public bool CanConsume(IItemConsumerRequest consumer);
        public bool AddItem(IItemProviderRequest provider);
        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer);
    }
}