using Game.Core.Execution;
using Infrastructure.Unity;
using UnityEngine;
using Game.Core.Audio;
using Primitives.EventBus.Abstractions;
using Physics.Core.PhysicsActors;
using Gameplay.Common.Unity;
using Core.Physics.Triggers.Callbacks;

namespace Unity.Common.Pickups
{
    public abstract class Pickup : TriggerVolume, ITriggerEnterEvent, IInitializable<IGameContext>
    {

        internal IAudioService _audioService;
        internal IEventBus _eventBus;
        [SerializeField] private int _priority = 8;
        public int Priority => _priority;

        private new void Awake()
        {
            base.Awake();
            RegistryGateway.Register<IInitializable<IGameContext>>(this);
        }
        public abstract void OnPickup(IPhysicsActor entity);
        public new void OnTriggerEntered(IPhysicsActor entity)
        {
            if (entity != null && entity.CompareTag(_triggerTarget))
            {
                OnPickup(entity);
                // Destroy(gameObject);
            }
        }

        private new void OnDestroy()
        {
            base.OnDestroy();
            RegistryGateway.Deregister<IInitializable<IGameContext>>(this);
        }

        public void Initialize(IGameContext context)
        {
            _audioService = context.AudioService;
            _eventBus = context.EventBus;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_audioService != null, $"Failed to set _audioService for: {gameObject.name}");
            Debug.Assert(_eventBus != null, $"Failed to set _eventBus for: {gameObject.name}");
        }
    }
}