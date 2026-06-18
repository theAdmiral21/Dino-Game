using System;
using Core.Equipment;
using Core.Inventory;
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
        public bool IsEquipable => _isEquipable;

        public IEquipment Equipment => _equipment;

        protected bool _isEquipable;

        protected int _maxAllowed;
        protected IEventBus _inventoryEventBus;
        private IEquipment _equipment;

        public BaseInventoryItem(ItemType item, int maxAllowed, IEventBus inventoryEventBus, IEquipment equipment)
        {
            Item = item;
            _maxAllowed = maxAllowed;
            _inventoryEventBus = inventoryEventBus;
            _equipment = equipment;

            _equipment.OnFire += HandleFire;
            _equipment.OnReload += HandleReload;
        }

        public abstract void HandleFire(int amount);
        public abstract void HandleReload(int requestedAmount, Action<int> replenishCallback);

        public int Deposit(int amount)
        {
            int availableSpace = _maxAllowed - amount;
            if (amount <= availableSpace)
            {
                IncrementQuantity(amount);
                return amount;
            }
            else
            {
                IncrementQuantity(availableSpace);
                return availableSpace;
            }

        }

        public int Withdraw(int amount)
        {
            int withdrawn = 0;
            if (Quantity >= amount)
            {
                withdrawn = amount;
            }
            else
            {
                withdrawn = Quantity;
            }

            DecrementQuantity(amount);

            return withdrawn;
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
                CurrentQuantity = Quantity
            });
            Debug.Log($"Emitted quantity changed event with amount: {Quantity}");
        }


    }
}