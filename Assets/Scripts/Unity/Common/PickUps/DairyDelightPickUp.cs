// using Game.Application.Audio.DataStructures;
// using Gameplay.Common.Core.DataStructures;
// using UnityEngine;
// using Physics.Core.Abstractions;
// using Primitives.Audio.SoundKeys;
// using Game.Core.ScoreSystem;
// using Primitives.Score;
// using Game.Core.Execution;
// using Game.Core.ScoreSystem.Events;
// using Game.Core.Modifiers;
// using Physics.Unity.Physics;
// using Movement.Core.Movement.DataStructures;
// using Physics.Core.PhysicsActors;
// using Physics.Unity.Actors;

// namespace Gameplay.Common.Unity.Pickups
// {
//     public class DairyDelightPickUp : Pickup, IZoomProvider, IScoreable, IInitializable<IGameContext>
//     {
//         [SerializeField] private ScoreObject _scoreType;
//         private IPointProvider _pointProvider;
//         [SerializeField] private float _zoomAmount;
//         public float ZoomAmount => _zoomAmount;

//         public int GetPoints()
//         {
//             return _pointProvider.ProvidePointValue(_scoreType);
//         }

//         public override void OnPickup(IPhysicsActor entity)
//         {
//             entity.EnqueueActionRequest(new AddZoomiesRequest(_zoomAmount));

//             _eventBus.Publish(new ScoreEvent(this));

//             _audioService.PlaySFX(new ItemSoundRequest(ItemSoundKey.DairyDelightPickUp));
//         }

//         public new void Initialize(IGameContext context)
//         {
//             base.Initialize(context);
//             _pointProvider = context.ScoreService.PointProvider;
//         }

//         public new void PostInitialize(IGameContext context)
//         {
//             base.PostInitialize(context);
//             Debug.Assert(_pointProvider != null, $"Failed to set _pointProvider for {gameObject.name}");
//         }
//     }
// }