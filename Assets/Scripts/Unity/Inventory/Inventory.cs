using Application.Inventory;
using Primitives.Items;
using Core.Inventory;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Primitives.EventBus.Abstractions;
using Infrastructure.Application.EventBus;
using Unity.Inventory.DataStructures;
using Core.Inventory.Requests;
using Core.Equipment;
using Unity.Common.Unity;

namespace Unity.Inventory
{
    public class Inventory : MonoBehaviour, IInventory
    {
        [SerializeField] private InventoryLimitSO _inventoryLimits;
        private Dictionary<ItemType, int> _limitMap = new();

        [SerializeField] private SerializedInterface<IEquipmentManager> _equipmentManagerMono;
        private IEquipmentManager _equipmentManager => _equipmentManagerMono.Interface;

        [SerializeField] private SerializedInterface<IEquipmentBridge> _equipmentBridgeMono;
        private IEquipmentBridge _equipmentBridge => _equipmentBridgeMono.Interface;

        [SerializeField] private bool _debug;
        private StringBuilder _debugSb = new();

        public IEventBus InventoryEventBus { get; private set; }
        public IInventorySystem InventorySystem { get; private set; }
        public IEquipment CurrentlyEquipped => _equipmentManager.ActiveEquipment;

        private void Awake()
        {
            _limitMap = _inventoryLimits.GetLimitMap();
            InventoryEventBus = new EventBus();
            InventorySystem = ConfigureInventory();

            // Init the equipment manager
            _equipmentManager.Init(InventoryEventBus, InventorySystem);
        }

        private IInventorySystem ConfigureInventory()
        {
            // Build the system
            IInventorySystem system = new InventorySystem(InventoryEventBus, _limitMap);

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

        // public bool TryEquip(ItemType item)
        // {
        //     return InventorySystem.TryEquip(item);
        // }

        public int StockItem(IItemProviderRequest provider)
        {
            return InventorySystem.RestockItem(provider);
        }
    }
}