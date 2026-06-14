using System.Collections.Generic;
using Core.Equipment;
using Core.Equipment.DataStructures;
using Movement.Application;
using Movement.Application.Abstractions;
using Movement.Application.Dispatchers;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class EquipmentOrchestrator : MonoBehaviour, IEquipmentOrchestrator
    {
        public IEquipmentBridge EquipmentBridge => _bridgeMono.Interface;
        [SerializeField] private SerializedInterface<IEquipmentBridge> _bridgeMono;

        private ActionDispatcher _actionDispatch;
        private List<IEquipmentActionResult> _results = new();
        private List<IEquipmentActionRequest> _requests = new();
        private EquipmentActionContext _equipmentContext;

        private void Awake()
        {
            _actionDispatch = new ActionDispatcher(BuildActionDispatchers());
        }
        public void EnqueueEquipmentRequest(IEquipmentActionRequest request)
        {
            _requests.Add(request);
        }

        public void ResolveRequests()
        {

        }

        private IDispatchRequest[] BuildActionDispatchers()
        {
            var dispatchers = new IDispatchRequest[]
            {
                new RaiseWeaponDispatcher(),
                new ShootDispatcher(),
        };

            return dispatchers;
        }

        public void Tick(float dt)
        {
            throw new System.NotImplementedException();
        }
    }
}