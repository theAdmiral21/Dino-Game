using Core.Equipment;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Physics.Core.PhysicsActors;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentBridge : SelfRegister<IInitializable<IGameContext>>, IEquipmentBridge, IInitializable<IGameContext>
    {
        [SerializeField] private SerializedInterface<IEquipmentManager> _equipmentManagerMono;
        private IEquipmentManager _equipmentManager => _equipmentManagerMono.Interface;

        public IEquipment Equipped => _equipped;
        private IEquipment _equipped => _equipmentManager.ActiveEquipment;

        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        private IActorEventBus _actorEventBus => _actorEventBusMono.Interface.ActorEventBus;

        public int Priority => 0;

        [Header("Debug")]
        [SerializeField] string CurrentWeapon;

        private void Awake()
        {
            base.Awake();

        }
        private void OnDestroy()
        {
            UnsubToEvents();
            base.OnDestroy();
        }

        public void Initialize(IGameContext context)
        {
            SubToEvents();
        }
        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_actorEventBus != null, "Failed to set actor event bus");
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
                        Equipped.RaiseWeapon(raiseWeapon.IsRaising);
                        break;
                    }
                case AimResult aim:
                    {
                        Debug.Log($"Asking to aim weapon");
                        Equipped.Aim(aim.MousePosition);
                        break;
                    }
                case ShootResult shoot:
                    {
                        Debug.Log($"Asking to shoot weapon");
                        Equipped.Fire();
                        break;
                    }
                case ReloadResult reload:
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

        private void SubToEvents()
        {
            _actorEventBus.OnEquipmentActionApproved += RouteEquipmentResult;
        }
        private void UnsubToEvents()
        {
            _actorEventBus.OnEquipmentActionApproved -= RouteEquipmentResult;
        }


    }
}