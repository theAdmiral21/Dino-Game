using System;
using Core.Environment.Events;
using Core.Environment.Interactions;
using Game.Core.Execution;
using Game.Unity.Events;
using Infrastructure.Unity;
using PlayerController.Unity.ManagerControls;
using Primitives.Environment;
using Primitives.EventBus.Abstractions;
using Unity.Environment;
using UnityEngine;

namespace Unity.Environment
{
    public class KeyDoor : Door, IKeyDoor, IInitializable<IGameContext>
    {
        [SerializeField] private KeyId _keyShape;

        private IEventBus _eventBus;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        protected override void Awake()
        {
            RegistryGateway.Register<IInitializable<IGameContext>>(this);
            base.Awake();
        }

        private void OnDestroy()
        {
            RegistryGateway.Deregister<IInitializable<IGameContext>>(this);
        }

        public void Initialize(IGameContext context)
        {
            _eventBus = context.EventBus;
            Debug.Assert(_eventBus != null, $"Unable to get event bus for {name}");
        }

        public void PostInitialize(IGameContext context)
        {
            _eventBus.Subscribe<UnlockEvent>(TryUnlock);
        }

        public void TryUnlock(UnlockEvent evt)
        {
            if (evt.Id == _keyShape)
            {
                SetLocked(false);
                return;
            }

        }
    }
}