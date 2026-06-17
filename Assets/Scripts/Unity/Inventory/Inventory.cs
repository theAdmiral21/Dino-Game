using Application.Inventory;
using Primitives.Items;
using Core.Inventory;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Primitives.EventBus.Abstractions;
using Infrastructure.Application.EventBus;
using Unity.Inventory.DataStructures;
using Unity.Equipment;
using Core.Inventory.Requests;
using Core.Equipment;
using PlasticPipe.PlasticProtocol.Messages;

namespace Unity.Inventory
{
    public class Inventory : MonoBehaviour, IInventory
    {
        [SerializeField] private InventoryLimitSO _inventoryLimits;
        private Dictionary<ItemType, int> _limitMap = new();

        [SerializeField] private bool _debug;
        private StringBuilder _debugSb = new();

        [SerializeField] private EquipmentFactory _equipmentFactory;

        public IEventBus InventoryEventBus { get; private set; }
        public IInventorySystem InventorySystem { get; private set; }
        public IEquipment CurrentlyEquipped => InventorySystem.CurrentlyEquipped.Equipment;

        private void Awake()
        {
            _limitMap = _inventoryLimits.GetLimitMap();
            InventoryEventBus = new EventBus();
            InventorySystem = ConfigureInventory();

            // Set the equipment bridge's event bus
            var bridge = GetComponent<IEquipmentBridge>();
            bridge.SetEventBus(InventoryEventBus);

        }

        private IInventorySystem ConfigureInventory()
        {
            // Still on the fence about making this inspector configurable. For now I'm doing this manually
            Dictionary<ItemType, IInventoryItem> itemsDict = new();
            // Build your list of inventory items
            // var rockInventory = new InventoryItem(ItemType.Rock, 5, InventoryEventBus);
            // items.Add(rockInventory);


            // use the limit map to get the first half of the inventory item
            foreach (var key in _limitMap.Keys)
            {
                InventoryItem newItem = new(key, _limitMap[key], InventoryEventBus, _equipmentFactory.BuildEquipment(key));
                itemsDict[key] = newItem;
            }


            // Build the system
            IInventorySystem system = new InventorySystem(InventoryEventBus, itemsDict);

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

        public int StockItem(IItemProviderRequest provider)
        {
            return InventorySystem.RestockItem(provider);
        }
    }
}