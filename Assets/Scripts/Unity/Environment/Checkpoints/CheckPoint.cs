using Environment.Core.Level;
using Game.Core.Events;
using Game.Core.Execution;
using Game.Unity.Events;
using Gameplay.Common.Unity;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using Physics.Core.PhysicsActors;
using PlayerController.Core.Info;
using Primitives.Checkpoints;
using Primitives.EventBus.Abstractions;
using Primitives.Infrastructure;
using UnityEngine;

namespace Environment.Unity.Checkpoints
{
    public class CheckPoint : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, ICheckpoint
    {
        [Header("Audio and Animation")]
        // NOTE I only have on serialized reference because if there are multiple on the same object, when I go to add it here, I can only add the top most interface... which is weird.
        [SerializeField] private AudioFeedBack _jingleFeedBack;
        [SerializeField] private ParticleFeedBack _particleFeedBack;
        [SerializeField] private AudioFeedBack _waterFeedBack;

        [Header("Collision Zones")]
        [SerializeField] private TriggerVolume _triggerCollider;
        public CheckpointId Id => _checkpointId;
        [SerializeField] private CheckpointId _checkpointId;
        public CheckPointData Data => _data;
        private CheckPointData _data;
        private IEventBus _eventBus;
        [SerializeField] private int _priority = 0;
        public int Priority => _priority;


        public void Awake()
        {
            base.Awake();
            RegistryGateway.Register<ICheckpoint>(this);

            _triggerCollider.OnVolumeEntered += UpdateCheckPoint;

            _data = new CheckPointData
            {
                Position = transform.position,
                Id = _checkpointId,
            };
        }
        public void OnDestroy()
        {
            RegistryGateway.Deregister<ICheckpoint>(this);

            if (_triggerCollider != null) _triggerCollider.OnVolumeEntered -= UpdateCheckPoint;

            base.OnDestroy();
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
            if (_jingleFeedBack != null) _jingleFeedBack.React();
            if (_waterFeedBack != null) _waterFeedBack.React();
            // Animate the bounce
            if (_particleFeedBack != null) _particleFeedBack.React();
        }

    }
}