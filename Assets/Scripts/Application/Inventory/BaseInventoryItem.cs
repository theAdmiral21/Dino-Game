using Core.Equipment;
using Core.Inventory;
using Core.Inventory.DataStructures.Providers;
using Core.Inventory.Requests;
using Primitives.EventBus.Abstractions;
using Primitives.Items;
using UnityEngine;

namespace Application.Inventory
{
    public abstract class BaseInventoryItem : IInventoryItem
    {
        public ItemType Item { get; private set; }

        public int Quantity { get; private set; }
        public bool PreviouslyFound { get; protected set; } = false;
        public bool IsEquipable => _isEquipable;
        protected bool _isEquipable;

        protected int _maxAllowed;
        protected IEventBus _inventoryEventBus;
        protected IEquipment _equipment;

        public BaseInventoryItem(ItemType item, int maxAllowed, IEventBus inventoryEventBus, IEquipment equipment)
        {
            Item = item;
            _maxAllowed = maxAllowed;
            _inventoryEventBus = inventoryEventBus;
            _equipment = equipment;
        }

        public void AddItem(IItemProviderRequest provider)
        {
            if (!CanAdd(provider)) return;

            IncrementQuantity(provider.Quantity);

            if (!PreviouslyFound)
            {
                PreviouslyFound = true;
                // Auto switch to the new item you just found
                _inventoryEventBus.Publish(new CurrentEquipmentChanged { NewItem = this, });
            }

        }

        public abstract bool CanAdd(IItemProviderRequest provider);

        public abstract bool CanConsume(IItemConsumerRequest consumer);
        public IEquipment GetEquipment()
        {
            return _equipment;
        }

        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer)
        {
            if (!CanConsume(consumer)) return null;

            switch (Item)
            {
                case ItemType.Shell:
                    {
                        DecrementQuantity(1);
                        return new ShellProvider();
                    }
                default:
                    {
                        Debug.LogError($"{consumer} is not a valid consumer request.");
                        return null;
                    }
            }
        }

        public void EquipItem()
        {
            _inventoryEventBus.Publish(new CurrentEquipmentChanged { NewItem = this });
        }

        private void DecrementQuantity(int amount)
        {
            Quantity -= amount;
            if (Quantity < 0)
            {
                Quantity = 0;
                return;
            }
            EmitQuantityChanged();
        }

        private void IncrementQuantity(int amount)
        {
            Quantity += amount;
            if (Quantity > _maxAllowed)
            {
                Quantity = _maxAllowed;
                return;
            }
            EmitQuantityChanged();
        }

        private void EmitQuantityChanged()
        {
            _inventoryEventBus.Publish(new EquipmentQuantityChanged
            {
                Item = Item,
                CurrentQuantity = Quantity
            });
            Debug.Log($"Emitted quantity changed event");
        }


    }
}