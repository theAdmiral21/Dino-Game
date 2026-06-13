using System;
using Application.Inventory;
using Primitives.Items;
using Core.Inventory;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Primitives.EventBus.Abstractions;
using Infrastructure.Application.EventBus;

namespace Unity.Inventory
{
    public class Inventory : MonoBehaviour, IInventory
    {
        public IEventBus InventoryEventBus { get; private set; }

        [SerializeField] private bool _debug;
        private StringBuilder _debugSb = new();
        public IInventorySystem InventorySystem { get; private set; }
        public ItemType CurrentlyEquipped => InventorySystem.CurrentlyEquipped;

        private void Awake()
        {
            InventoryEventBus = new EventBus();
            InventorySystem = ConfigureInventory();
        }

        private IInventorySystem ConfigureInventory()
        {
            // Still on the fence about making this inspector configurable. For now I'm doing this manually
            List<IInventoryItem> items = new();
            // Build your list of inventory items
            var rockInventory = new InventoryItem(ItemType.Rock, 5, InventoryEventBus);
            items.Add(rockInventory);

            // Build the system
            IInventorySystem system = new InventorySystem(items);

            return system;
        }

        private void LateUpdate()
        {
            _debugSb.Clear();
            _debugSb.AppendLine($"Item: Quantity");
            if (_debug)
            {
                foreach (var key in InventorySystem.Items.Keys)
                {
                    _debugSb.AppendLine($"{key}: {InventorySystem.Items[key].Quantity}");
                }
                Debug.Log(_debugSb);
            }
        }

        public bool TryEquip(ItemType item)
        {
            return InventorySystem.TryEquip(item);
        }
    }
}