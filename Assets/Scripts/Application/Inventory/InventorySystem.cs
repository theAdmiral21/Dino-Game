using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;
using Core.Inventory.DataStructures.Consumers;
using Core.Inventory.DataStructures.Providers;
using Core.Inventory.Requests;
using Primitives.Items;

namespace Application.Inventory
{
    public class InventorySystem : IInventorySystem
    {
        public Dictionary<ItemType, IInventoryItem> Items => _items;
        public ItemType CurrentlyEquipped { get; private set; } = ItemType.None;
        private Dictionary<ItemType, IInventoryItem> _items = new();

        public InventorySystem(List<IInventoryItem> inventoryItems)
        {
            foreach (var item in inventoryItems)
            {
                if (!_items.TryAdd(item.Item, item))
                {
                    throw new ArgumentException($"Key: {item} already exists in {Items}");
                }
            }
        }

        public bool AddItem(IItemProviderRequest provider)
        {
            Debug.Log($"Got provider: {provider}");
            // switch (provider)
            // {
            //     case ShellProvider shellProvider:
            //         {
            if (_items[provider.Item].CanAdd(provider))
            {
                Debug.Log($"Adding {provider}");
                _items[provider.Item].AddItem(provider);
                return true;
            }
            return false;
            //     }
            // case RockProvider rockProvider:
            //     {
            //         if (_items[provider.Item].CanAdd(rockProvider))
            //         {
            //             _items[provider.Item].AddItem(rockProvider);
            //             return true;
            //         }
            //         return false;
            //     }
            // default:
            //     {
            //         Debug.LogError($"{provider} is not a valid provider request.");
            //         return false;
            //     }
            // }
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
                    CurrentlyEquipped = item;
                    inventoryItem.EquipItem();
                    return true;
                }
            }
            return false;

        }
    }
}
