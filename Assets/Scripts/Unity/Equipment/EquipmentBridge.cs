using System.Collections.Generic;
using Core.Equipment;
using Core.Inventory;
using Movement.Core.Movement.DataStructures;
using Primitives.EventBus.Abstractions;
using Primitives.Items;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentBridge : MonoBehaviour, IEquipmentBridge
    {
        [SerializeField] private SerializedInterface<IInventory> _inventoryMono;
        public IEquipment Equipped => _inventorySystem.Items[_inventorySystem.CurrentlyEquipped].GetEquipment();
        private IInventorySystem _inventorySystem;// => _inventoryMono.Interface.InventorySystem;
        private void Awake()
        {
            var inventory = GetComponent<IInventory>();
            _inventorySystem = inventory.InventorySystem;
        }

        public void RouteEquipmentResult(IEquipmentActionResult result)
        {
            Debug.Log($"Switching on result: {result}");
            if (_inventorySystem.CurrentlyEquipped == ItemType.None) return;
            switch (result)
            {
                case RaiseWeaponResult raiseWeapon:
                    {
                        Debug.Log($"Asking to raise weapon");
                        Equipped.RaiseWeapon();
                        break;
                    }
                // case AimResult raiseWeapon:
                //     {
                //         Equipped.Aim();
                //         break;
                //     }
                case ShootResult shoot:
                    {
                        Debug.Log($"Asking to shoot weapon");
                        Equipped.Fire();
                        break;
                    }
                    // case ReloadResult raiseWeapon:
                    //     {
                    //         Equipped.Reload();
                    //         break;
                    //     }
            }
        }
    }
}