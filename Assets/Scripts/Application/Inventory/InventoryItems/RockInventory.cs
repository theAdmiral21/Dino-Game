using System;
using UnityEngine;
using Core.Equipment;
using Core.Inventory;
using Primitives.EventBus.Abstractions;
using Primitives.Items;

namespace Application.Inventory.InventoryItems
{
    public class RockInventory : BaseInventoryItem
    {
        public RockInventory(ItemType item, int maxAllowed, IEventBus inventoryEventBus, IEquipment equipment) : base(item, maxAllowed, inventoryEventBus, equipment)
        {
        }

        public override void HandleFire(int amount)
        {
            _inventoryEventBus.Publish(new MagazineQuantityChanged
            {
                CurrentQuantity = amount
            });
        }

        public override void HandleReload(int requestedAmount, Action<int> replenishCallback)
        {
            int withdrawn = Withdraw(requestedAmount);
            Debug.Log($"Withdrew {withdrawn} rocks");
            replenishCallback?.Invoke(withdrawn);
            _inventoryEventBus.Publish(new MagazineQuantityChanged
            {
                CurrentQuantity = withdrawn
            }
            );
        }
    }
}