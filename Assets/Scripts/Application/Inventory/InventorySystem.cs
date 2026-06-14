using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;
using Core.Inventory.DataStructures.Consumers;
using Core.Inventory.DataStructures.Providers;
using Core.Inventory.Requests;
using Primitives.Items;
using Primitives.EventBus.Abstractions;

namespace Application.Inventory
{
    public class InventorySystem : IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items => _items;
        public ItemType CurrentlyEquipped => _currentItem == null ? ItemType.None : _currentItem.Item;
        private Dictionary<ItemType, IInventoryItem> _items = new();
        private IEventBus _inventoryEventBus;
        private IInventoryItem _currentItem;
        public InventorySystem(List<IInventoryItem> inventoryItems, IEventBus inventoryEventBus)
        {
            _inventoryEventBus = inventoryEventBus;
            SubToEvents();

            foreach (var item in inventoryItems)
            {
                if (!_items.TryAdd(item.Item, item))
                {
                    throw new ArgumentException($"Key: {item} already exists in {Items}");
                }
            }
        }
        private void SubToEvents()
        {
            _inventoryEventBus.Subscribe<CurrentEquipmentChanged>(HandleEquipmentChange);
        }

        public bool AddItem(IItemProviderRequest provider)
        {
            Debug.Log($"Got provider: {provider}");

            if (_items[provider.Item].CanAdd(provider))
            {
                Debug.Log($"Adding {provider}");
                _items[provider.Item].AddItem(provider);
                return true;
            }
            return false;

        }


        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer)
        {
            switch (consumer)
            {
                case ShellConsumer shellRequest:
                    {
                        return _items[consumer.Item].ConsumeItem(shellRequest);
                    }
                case RockConsumer rockRequest:
                    {
                        return _items[consumer.Item].ConsumeItem(rockRequest);
                    }
                default:
                    {
                        Debug.LogError($"{consumer} is not a valid consumer request.");
                        return null;
                    }
            }
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
                if (inventoryItem.Quantity > 0)
                {
                    // equip the item
                    // inventoryItem.EquipItem();
                    _currentItem = inventoryItem;
                    return true;
                }
            }
            return false;

        }
        private void HandleEquipmentChange(CurrentEquipmentChanged evt)
        {
            TryEquip(evt.NewItem.Item);
        }

    }
}
