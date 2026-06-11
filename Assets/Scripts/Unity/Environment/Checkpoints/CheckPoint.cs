using Environment.Core.Level;
using Game.Core.Events;
using Game.Core.Execution;
using Game.Unity.Events;
using Gameplay.Common.Unity;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Physics.Unity.Actors;
using Physics.Unity.Physics;
using PlayerController.Core.Info;
using Primitives.EventBus.Abstractions;
using Primitives.Infrastructure;
using Unity.Common.Unity;
using UnityEngine;

namespace Environment.Unity.Checkpoints
{
    public class CheckPoint : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, ICheckpoint
    {
        [Header("Audio and Animation")]
        // NOTE I only have on serialized reference because if there are multiple on the same object, when I go to add it here, I can only add the top most interface... which is weird.
        [SerializeField] private SerializedInterface<IEventFeedBack> _jingleFeedBack;
        [SerializeField] private ParticleFeedBack _particleFeedBack;
        [SerializeField] private AudioFeedBack _waterFeedBack;

        [Header("Collision Zones")]
        [SerializeField] private TriggerVolume _triggerCollider;
        public bool IsDebugStart => false;
        public bool IsLevelStart => false;
        public CheckPointData Data => _data;
        private CheckPointData _data;
        private IEventBus _eventBus;
        public int Priority => 0;


        public void Awake()
        {
            base.Awake();
            RegistryGateway.Register<ICheckpoint>(this);

            _triggerCollider.OnVolumeEntered += UpdateCheckPoint;

            _data = new CheckPointData
            {
                LevelStart = IsLevelStart,
                Position = transform.position,
                CheckPointId = GetHashCode(),
            };
        }
        public void OnDestroy()
        {
            RegistryGateway.Deregister<ICheckpoint>(this);

            if (_triggerCollider != null) _triggerCollider.OnVolumeEntered -= UpdateCheckPoint;
        }

        public void Initialize(IGameContext context)
        {
            _eventBus = context.EventBus;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_eventBus != null, "Failed to assign event bus");
        }

        public void UpdateCheckPoint(IPhysicsActor actor)
        {
            var playerInfo = actor.GetComponent<IPlayerInfoProvider>();


            // Notify interested parties about the checkpoint
            _eventBus.Publish(new CheckPointTriggeredEvent(_data, playerInfo.PlayerInfo.PlayerId));

            // Play audio
            if (_jingleFeedBack != null) _jingleFeedBack.Interface.React();
            if (_waterFeedBack != null) _waterFeedBack.React();
            // Animate the bounce
            if (_particleFeedBack != null) _particleFeedBack.React();
        }

    }
}