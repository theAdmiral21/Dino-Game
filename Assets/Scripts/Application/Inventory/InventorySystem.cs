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

        public void AddItem(IItemProviderRequest provider)
        {
            switch (provider)
            {
                case ShellProvider shellProvider:
                    {
                        _items[provider.Item].AddItem(shellProvider);
                        break;
                    }
            }
        }

        public IItemProviderRequest ConsumeItem(IItemConsumerRequest consumer)
        {
            switch (consumer)
            {
                case ShellRequest shellRequest:
                    {
                        return _items[consumer.Item].ConsumeItem(shellRequest);
                    }
                default:
                    {
                        Debug.LogError($"{consumer} is not a valid consumer request.");
                        return null;
                    }
            }
        }
    }
}
