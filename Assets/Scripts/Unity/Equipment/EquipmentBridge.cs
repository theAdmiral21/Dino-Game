using System;
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
        [SerializeField] private SerializedInterface<IEquipmentManager> _equipmentManagerMono;
        private IEquipmentManager _equipmentManager => _equipmentManagerMono.Interface;

        public IEquipment Equipped => _equipped;
        private IEquipment _equipped => _equipmentManager.ActiveEquipment;
        private IEventBus _inventoryEventBus;

        [Header("Debug")]
        [SerializeField] string CurrentWeapon;

        private void OnDestroy()
        {
            UnsubToEvents();
        }

        public void RouteEquipmentResult(IEquipmentActionResult result)
        {
            Debug.Log($"Switching on result: {result}");
            if (Equipped == null) return;

            switch (result)
            {
                case RaiseWeaponResult raiseWeapon:
                    {
                        Debug.Log($"Asking to raise weapon");
                        Equipped.RaiseWeapon();
                        break;
                    }
                case AimResult aim:
                    {
                        Equipped.Aim(aim.MousePosition);
                        break;
                    }
                case ShootResult shoot:
                    {
                        Debug.Log($"Asking to shoot weapon");
                        Equipped.Fire();
                        break;
                    }
                case ReloadResult raiseWeapon:
                    {
                        Equipped.RequestReload();
                        break;
                    }
            }
        }

        private void LateUpdate()
        {
            try
            {
                CurrentWeapon = $"{_equipped.EquipmentType}";
            }
            catch
            {
                CurrentWeapon = $"None";
            }
        }

        public void SetEventBus(IEventBus eventBus)
        {
            _inventoryEventBus = eventBus;
            SubToEvents();
        }

        private void SubToEvents()
        {
            _inventoryEventBus.Subscribe<CurrentEquipmentChanged>(UpdateEquipment);
        }
        private void UnsubToEvents()
        {
            _inventoryEventBus.Unsubscribe<CurrentEquipmentChanged>(UpdateEquipment);
        }

        private void UpdateEquipment(CurrentEquipmentChanged changed)
        {
            // _equipped = 
        }
    }
}