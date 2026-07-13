// using Environment.Core.Level;
// using Game.Core.Events;
// using Game.Core.Execution;
// using Gameplay.Common.Unity;
// using Infrastructure.Unity;
// using Infrastructure.Unity.Registries;
// using Physics.Core.PhysicsActors;
// using PlayerController.Core.Info;
// using Primitives.EventBus.Abstractions;
// using Primitives.Infrastructure;
// using UnityEngine;

// namespace Environment.Unity.Checkpoints
// {
//     public class LevelStart : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, ICheckpoint
//     {
//         public Vector2 Spawn => transform.position;

//         [Header("Collision Zones")]
//         [SerializeField] private TriggerVolume _triggerCollider;
//         public bool IsDebugStart => false;
//         public bool IsLevelStart => true;

//         public CheckPointData Data => _data;
//         private CheckPointData _data;


//         private IEventBus _eventBus;
//         [SerializeField] private int _priority = 0;
//         public int Priority => _priority;
//         private void Awake()
//         {
//             base.Awake();
//             RegistryGateway.Register<ICheckpoint>(this);
//             _triggerCollider.OnVolumeEntered += UpdateCheckPoint;

//             _data = new CheckPointData
//             {
//                 LevelStart = IsLevelStart,
//                 Position = transform.position,
//                 CheckPointId = GetHashCode(),
//             };
//         }
//         public void OnDestroy()
//         {
//             RegistryGateway.Deregister<ICheckpoint>(this);

//             if (_triggerCollider != null) _triggerCollider.OnVolumeEntered -= UpdateCheckPoint;
//         }

//         public void Initialize(IGameContext context)
//         {
//             _eventBus = context.EventBus;
//         }

//         public void PostInitialize(IGameContext context)
//         {
//             Debug.Assert(_eventBus != null, "Failed to assign event bus");
//         }

//         public void UpdateCheckPoint(IPhysicsActor actor)
//         {
//             var playerInfo = actor.GetComponent<IPlayerInfoProvider>();


//             // Notify interested parties about the checkpoint
//             _eventBus.Publish(new CheckPointTriggeredEvent(_data, playerInfo.PlayerInfo.PlayerId));
//         }
//     }
// }