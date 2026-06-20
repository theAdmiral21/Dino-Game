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
        [SerializeField] private GameObject _rockPrefab;
        // [SerializeField] private SerializedInterface<IInventory> _inventorySO;
        // private IInventorySystem _inventorySystem => _inventorySO.Interface.InventorySystem;

        public IEquipment BuildEquipment(ItemType item, Transform anchor)
        {
            if (_statMap == null) _statMap = _mapSO.GetStatMap();

            switch (item)
            {
                case ItemType.Rock:
                    {
                        GameObject rock = Instantiate(_rockPrefab, anchor);
                        Debug.Assert(rock != null, "Why is rock null?");
                        IEquipment equipment = rock.GetComponent<IEquipment>();
                        equipment.Init(_statMap[item]);
                        Debug.Assert(equipment != null, "Why is equipment null?");
                        return equipment;
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