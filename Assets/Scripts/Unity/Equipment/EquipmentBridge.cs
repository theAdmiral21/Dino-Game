using System.Collections.Generic;
using Core.Equipment;
using Core.Inventory;
using Movement.Core.Movement.DataStructures;
using Primitives.EventBus.Abstractions;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentBridge : MonoBehaviour, IEquipmentBridge
    {
        public IEquipment Equipped => _inventorySystem.Items[_inventorySystem.CurrentlyEquipped].GetEquipment();
        private IInventorySystem _inventorySystem;
        private void Awake()
        {
            var inventory = GetComponent<IInventory>();
            _inventorySystem = inventory.InventorySystem;
        }

        public void RouteEquipmentResult(IEquipmentActionResult result)
        {
            switch (result)
            {
                case RaiseWeaponResult raiseWeapon:
                    {
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