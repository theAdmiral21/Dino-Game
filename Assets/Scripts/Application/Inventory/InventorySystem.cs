using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;
using Core.Inventory.Requests;
using Primitives.Items;
using Primitives.EventBus.Abstractions;

namespace Application.Inventory
{
    public class InventorySystem : IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items => _items;
        private Dictionary<ItemType, IInventoryItem> _items = new();
        private readonly Dictionary<ItemType, int> _itemLimits = new();
        public IInventoryItem CurrentlyEquipped => _currentItem;
        private IInventoryItem _currentItem;

        private IEventBus _inventoryEventBus;
        public InventorySystem(IEventBus inventoryEventBus, Dictionary<ItemType, int> limitMap)
        {
            _inventoryEventBus = inventoryEventBus;
            _itemLimits = limitMap;
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
                IInventoryItem newItem = BuildNewInventoryItem(provider.Item);
                _items[provider.Item] = newItem;

                _inventoryEventBus.Publish(new CurrentEquipmentChanged { NewItem = newItem });
            }
            int deposited = _items[provider.Item].Deposit(provider.Quantity);
            return deposited;
        }

        private IInventoryItem BuildNewInventoryItem(ItemType item)
        {
            int itemLimit = _itemLimits[item];

            return new InventoryItem(item, itemLimit, _inventoryEventBus);
        }

        private bool TryEquip(ItemType item)
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

        // private void EmitEquippedQuantityChanged(int newQuantity)
        // {
        //     Debug.Log($"Emitting equipped quantity changed with value {newQuantity}");
        //     _inventoryEventBus.Publish(new EquipmentQuantityChanged
        //     {
        //         // InventoryItem = item,
        //         CurrentQuantity = newQuantity
        //     });
        //     Debug.Log($"Emitted quantity changed event");
        // }
    }
}
