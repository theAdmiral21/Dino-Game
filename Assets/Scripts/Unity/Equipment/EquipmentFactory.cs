using System.Collections.Generic;
using Core.Equipment;
using Core.Inventory;
using Primitives.Items;
using Unity.Common.Unity;
using Unity.Equipment.DataStructures;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentFactory : MonoBehaviour
    {
        [SerializeField] private EquipmentMapSO _mapSO;
        private Dictionary<ItemType, EquipmentStats> _statMap;

        [SerializeField] private SerializedInterface<IInventory> _inventorySO;
        private IInventorySystem _inventorySystem => _inventorySO.Interface.InventorySystem;

        public IEquipment BuildEquipment(ItemType item)
        {
            if (_statMap == null) _statMap = _mapSO.GetStatMap();

            switch (item)
            {
                case ItemType.Rock:
                    {
                        var rock = new RockEquipment(item, _statMap[item], _inventorySystem);
                        return rock;
                    }
                default:
                    {
                        Debug.LogError($"Item type: {item} is not currently supported.");
                        return null;
                    }
            }
        }
    }
}