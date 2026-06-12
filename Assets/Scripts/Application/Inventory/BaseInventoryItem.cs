using Core.Inventory.DataStructures.Providers;
using Core.Inventory.Requests;
using Primitives.Items;
using UnityEngine;

namespace Application.Inventory
{
    public abstract class BaseInventoryItem
    {
        public ItemType Item { get; private set; }

        public int Quantity { get; private set; }

        protected int _maxAllowed;

        public BaseInventoryItem(ItemType item, int maxAllowed)
        {
            Item = item;
            _maxAllowed = maxAllowed;
        }

        public void AddItem(IItemProviderRequest provider)
        {
            if (!CanAdd(provider)) return;
            IncrementQuantity();
        }

        public abstract bool CanAdd(IItemProviderRequest provider);

        public abstract bool CanConsume(IItemConsumerRequest consumer);

        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer)
        {
            if (!CanConsume(consumer)) return null;

            switch (Item)
            {
                case ItemType.Shell:
                    {
                        DecrementQuantity();
                        return new ShellProvider();
                    }
                default:
                    {
                        Debug.LogError($"{consumer} is not a valid consumer request.");
                        return null;
                    }
            }
        }

        private void DecrementQuantity()
        {
            Quantity -= 1;
            if (Quantity < 0) Quantity = 0;
        }

        private void IncrementQuantity()
        {
            Quantity += 1;
        }
    }
}