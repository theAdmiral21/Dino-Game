using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;
using Core.Inventory.Requests;
using Primitives.Items;
using Primitives.EventBus.Abstractions;
using Movement.Core.Movement.DataStructures;

namespace Application.Inventory
{
    public class InventorySystem : IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items => _items;
        private Dictionary<ItemType, IInventoryItem> _items = new();

        private readonly Dictionary<ItemType, int> _itemLimits = new();
        private readonly Dictionary<int, ItemType> _indexMap = new()
        {
          {1, ItemType.Rock},
          {2, ItemType.Taser},
          {3,ItemType.Shotgun},
          {4,ItemType.SmokeGrenade},
          {5,ItemType.Flares},
          {6,ItemType.NerveGas},
          {7,ItemType.RocketLauncher}
        };

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
                // Build the new item
                IInventoryItem newItem = BuildNewInventoryItem(provider.Item);
                // Add the item to the inventory system
                _items[provider.Item] = newItem;
                // Attempt to equip the new item
                TryEquip(provider.Item);
            }
            int deposited = _items[provider.Item].Deposit(provider.Quantity);
            return deposited;
        }

        public void SwitchEquipment(SwitchEquipmentResult switchEquipment)
        {
            // Try and equip the new item
            ItemType item = _indexMap[switchEquipment.EquipmentNdx];
            bool res = TryEquip(item);

            if (!res) Debug.Log($"Indicate the failed equipment switch some how");
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
                // Equip the item
                _currentItem = inventoryItem;

                // Let everyone know you equipped a new item
                _inventoryEventBus.Publish(new CurrentEquipmentChanged { NewItem = _items[item] });
                return true;
            }
        }
        private void HandleEquipmentChange(CurrentEquipmentChanged evt)
        {
            Debug.Log($"Got equipment changed event");
            // bool res = TryEquip(evt.NewItem.Item);
            // Debug.Log($"Equip result: {res}");
        }
    }
}
