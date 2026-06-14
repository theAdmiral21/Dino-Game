using System.Collections.Generic;
using Primitives.Items;
using Core.Equipment;
using Core.Inventory.Requests;

namespace Application.Inventory
{
    public class Magazine : IMagazine
    {
        public ItemType AmmoType { get; private set; }

        public int RoundCount => _magazine.Count;

        public int Capacity { get; private set; }

        private Stack<IItemProviderRequest> _magazine = new();
        public Magazine(ItemType ammoType, int capacity)
        {
            AmmoType = ammoType;
            Capacity = capacity;
        }

        public bool AddItem(IItemProviderRequest provider)
        {
            if (!CanAdd(provider)) return false;

            _magazine.Push(provider);
            return true;
        }

        public bool CanAdd(IItemProviderRequest provider)
        {
            if (provider.Item != AmmoType) return false;
            if (RoundCount >= Capacity) return false;

            return true;
        }

        public bool CanConsume(IItemConsumerRequest consumer)
        {
            if (RoundCount <= 0) return false;

            return true;
        }

        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer)
        {
            if (!CanConsume(consumer)) return null;

            return _magazine.Pop();
        }
    }
}