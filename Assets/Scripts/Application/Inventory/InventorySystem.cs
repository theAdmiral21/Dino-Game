using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;
using Core.Inventory.Requests;
using Primitives.Items;
using Primitives.EventBus.Abstractions;
using Movement.Core.Movement.DataStructures;
using Primitives.SaveData;
using UnityEngine.InputSystem.EnhancedTouch;
using System;

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

        private int _currentIndex => _indexMapReverse[_currentItem.Item];
        private readonly Dictionary<ItemType, int> _indexMapReverse = new()
        {
          { ItemType.Rock, 1},
          { ItemType.Taser, 2},
          {ItemType.Shotgun, 3},
          {ItemType.SmokeGrenade, 4},
          {ItemType.Flares, 5},
          {ItemType.NerveGas, 6},
          {ItemType.RocketLauncher, 7}
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
        public void UpdateInventoryContents(InventorySaveData saveData)
        {
            // Rebuild the inventory data
            Dictionary<ItemType, IInventoryItem> itemDict = new();
            for (int i = 0; i < saveData.Items.Count; i++)
            {
                ItemType key = saveData.Items[i].Item;
                itemDict[key] = BuildNewInventoryItem(key);
            }
            // Overwrite the current dict
            _items = itemDict;
            // Equip the item
            bool res = TryEquip(saveData.CurrentItem);
            Debug.Assert(res == true, $"Failed to equip equipment after revert.");
        }
        public void Dispose()
        {
            Debug.Log($"Destroying InventorySystem: {GetHashCode()}");
            UnSubToEvents();
        }

        private void SubToEvents()
        {
            _inventoryEventBus.Subscribe<CurrentEquipmentChanged>(HandleEquipmentChange);
        }
        private void UnSubToEvents()
        {
            _inventoryEventBus.Unsubscribe<CurrentEquipmentChanged>(HandleEquipmentChange);
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

        public void IndexEquipment(IndexEquipmentResult indexEquipment)
        {
            int direction = indexEquipment.DeltaNdx >= 0 ? 1 : -1;
            Debug.Assert(_indexMapReverse != null, "_indexMapReverse is null");
            Debug.Assert(_indexMap != null, "_indexMap is null");
            Debug.Assert(_currentItem != null, $"_currentItem is null for {GetHashCode()}");
            int startIndex = _currentIndex;

            for (int attempt = 1; attempt <= 7; attempt++)
            {
                int ndx = ((startIndex - 1 + direction * attempt) % 7 + 7) % 7 + 1;
                ItemType item = _indexMap[ndx];

                if (TryEquip(item))
                {
                    return;
                }
            }

            // Tried all 7 slots, nothing else equippable — stay on current item
            Debug.Log("No other equippable item found; keeping current equipment.");
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

                Debug.Log($"Current item: {_currentItem.Item} for {GetHashCode()}");
                Debug.Log($"Current Index: {_currentIndex}");
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
