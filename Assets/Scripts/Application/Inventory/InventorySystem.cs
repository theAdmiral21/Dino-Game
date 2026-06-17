using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;
using Core.Inventory.Requests;
using Primitives.Items;
using Primitives.EventBus.Abstractions;
using Core.Inventory.DataStructures.Providers;

namespace Application.Inventory
{
    public class InventorySystem : IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items => _items;
        private Dictionary<ItemType, IInventoryItem> _items = new();
        private readonly Dictionary<ItemType, IInventoryItem> _refItems = new();
        public IInventoryItem CurrentlyEquipped => _currentItem;
        private IInventoryItem _currentItem;

        private IEventBus _inventoryEventBus;
        public InventorySystem(IEventBus inventoryEventBus, Dictionary<ItemType, IInventoryItem> refItems)
        {
            _inventoryEventBus = inventoryEventBus;
            _refItems = refItems;
            SubToEvents();

            // Assign a default piece of equipment
            // RestockItem(new TaserProvider(0));
        }
        private void SubToEvents()
        {
            _inventoryEventBus.Subscribe<CurrentEquipmentChanged>(HandleEquipmentChange);
        }



        public int RestockItem(IItemProviderRequest provider)
        {
            // If this is the first time collecting this item, emit an event
            if (!_items.ContainsKey(provider.Item))
            {
                // Ugh this should probably be a factory
                _items[provider.Item] = _refItems[provider.Item];
                var newItem = _items[provider.Item];
                _inventoryEventBus.Publish(new CurrentEquipmentChanged { NewItem = newItem });
            }
            int deposited = _items[provider.Item].Deposit(provider.Quantity);
            EmitEquippedQuantityChanged(deposited);
            return deposited;
        }


        public int ConsumeItem(IItemConsumerRequest consumer)
        {
            int withdrawn = _currentItem.Withdraw(consumer.WithdrawAmount);
            EmitEquippedQuantityChanged(withdrawn);
            return withdrawn;
        }

        public bool TryEquip(ItemType item)
        {
            // try to get the proposed item
            if (!Items.TryGetValue(item, out var inventoryItem))
            {
                return false;
            }
            else
            {
                // check the quantity of the item
                // if (inventoryItem.Quantity > 0)
                // {
                // equip the item
                // inventoryItem.EquipItem();
                _currentItem = inventoryItem;
                return true;
                // }
            }
            // return false;

        }
        private void HandleEquipmentChange(CurrentEquipmentChanged evt)
        {
            Debug.Log($"Got equipment changed event");
            bool res = TryEquip(evt.NewItem.Item);
            Debug.Log($"Equip result: {res}");
        }

        private void EmitEquippedQuantityChanged(int newQuantity)
        {
            Debug.Log($"Emitting equipped quantity changed with value {newQuantity}");
            _inventoryEventBus.Publish(new EquipmentQuantityChanged
            {
                // InventoryItem = item,
                CurrentQuantity = newQuantity
            });
            Debug.Log($"Emitted quantity changed event");
        }
    }
}
