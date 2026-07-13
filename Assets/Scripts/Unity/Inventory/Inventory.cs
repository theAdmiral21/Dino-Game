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
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Core.Game.Lifecycle;
using Primitives.SaveData;
using Infrastructure.Unity;
using System.Linq;

namespace Unity.Inventory
{
    public class Inventory : SelfRegister<IInitializable<IGameContext>>, IInventory, IInitializable<IGameContext>, IRevertable
    {
        [SerializeField] private InventoryLimitSO _inventoryLimits;
        private Dictionary<ItemType, int> _limitMap = new();

        [SerializeField] private SerializedInterface<IEquipmentManager> _equipmentManagerMono;
        private IEquipmentManager _equipmentManager => _equipmentManagerMono.Interface;

        [SerializeField] private SerializedInterface<IEquipmentBridge> _equipmentBridgeMono;
        private IEquipmentBridge _equipmentBridge => _equipmentBridgeMono.Interface;

        [SerializeField] private bool _debug;
        private StringBuilder _debugSb = new();

        public IEventBus EventBus { get; private set; }
        public IInventorySystem InventorySystem { get; private set; }
        public IEquipment CurrentlyEquipped => _equipmentManager.ActiveEquipment;

        private InventorySaveData? _saveData;
        [SerializeField] private int _priority;
        public int Priority => _priority;


        private void Awake()
        {
            base.Awake();
            _limitMap = _inventoryLimits.GetLimitMap();
            RegistryGateway.Register<IRevertable>(this);
        }

        public void Initialize(IGameContext context)
        {
            EventBus = context.EventBus;
        }

        public void PostInitialize(IGameContext context)
        {
            // This class doesn't provide anything other classes need during initialize, so configure it in post initialize
            InventorySystem = ConfigureInventory();
            // Init the equipment manager
            _equipmentManager.Init(EventBus, InventorySystem);
        }

        private IInventorySystem ConfigureInventory()
        {
            // Build the system
            IInventorySystem system = new InventorySystem(EventBus, _limitMap);

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
            if (provider.Item == ItemType.Flashlight)
            {
                _equipmentManager.AllowFlashLight();
                return 1; // this consume the pick up
            }
            return InventorySystem.RestockItem(provider);
        }

        public void Revert()
        {
            if (_saveData.HasValue)
            {
                InventorySystem = new InventorySystem(EventBus, _limitMap, _saveData.Value);
            }
            else
            {
                Debug.LogError($"No serialized data to get.");
            }
        }

        public void TakeSnapShot()
        {
            // flatten the dictionary
            List<ItemType> keys = InventorySystem.Items.Keys.ToList();
            List<ItemStock> items = new();
            for (int i = 0; i < keys.Count; i++)
            {
                items.Add(new ItemStock { Item = keys[i], Amount = InventorySystem.Items[keys[i]].Quantity });
            }

            _saveData = new InventorySaveData
            {
                CurrentItem = InventorySystem.CurrentlyEquipped.Item,
                Items = items
            };
        }

        public void SerializeSnapShot()
        {
            throw new System.NotImplementedException();
        }
    }
}