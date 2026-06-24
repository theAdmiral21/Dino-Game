using System;
using Core.Inventory;
using Primitives.EventBus.Abstractions;
using Primitives.Items;

namespace Application.Inventory
{
    public class InventoryItem : IInventoryItem
    {
        public ItemType Item { get; private set; }

        public int Quantity { get; private set; }


        protected int _maxAllowed;
        protected IEventBus _inventoryEventBus;

        public InventoryItem(ItemType item, int maxAllowed, IEventBus inventoryEventBus)
        {
            Item = item;
            _maxAllowed = maxAllowed;
            _inventoryEventBus = inventoryEventBus;

        }

        public void HandleFire(int amount)
        {
            _inventoryEventBus.Publish(new MagazineQuantityChanged
            {
                CurrentQuantity = amount
            });
        }

        public void HandleReload(int requestedAmount, Action<int> replenishCallback)
        {
            int withdrawn = Withdraw(requestedAmount);
            // Debug.Log($"Withdrew {withdrawn} rocks");
            replenishCallback?.Invoke(withdrawn);
            _inventoryEventBus.Publish(new MagazineQuantityChanged
            {
                CurrentQuantity = withdrawn
            }
            );
        }

        public int Deposit(int amount)
        {
            // Check how much room we have
            int availableSpace = _maxAllowed - Quantity;

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
            // Debug.Log($"Emitted quantity changed event with amount: {Quantity}");
        }


    }
}