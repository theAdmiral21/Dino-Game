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

        [SerializeField] private Transform _equipmentTransform;
        [SerializeField] private GameObject _rockPrefab;
        // [SerializeField] private SerializedInterface<IInventory> _inventorySO;
        // private IInventorySystem _inventorySystem => _inventorySO.Interface.InventorySystem;

        public IEquipment BuildEquipment(ItemType item)
        {
            if (_statMap == null) _statMap = _mapSO.GetStatMap();

            switch (item)
            {
                // case ItemType.Rock:
                //     {
                //         GameObject rock = Instantiate(_rockPrefab, _equipmentTransform);
                //         return rock.GetComponent<IEquipment>();
                //     }
                case ItemType.Rock:
                    {
                        var rock = new RockEquipment(_statMap[item]);
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