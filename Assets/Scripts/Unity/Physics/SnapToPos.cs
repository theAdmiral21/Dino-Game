using Core.Environment.Interactions;
using Core.Physics.PhysicsQueries;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Physics.Core.PhysicsActors;
using Physics.Core.Services;
using Unity.Common;
using Unity.Common.Unity;
using Unity.Infrastructure.Providers;
using UnityEngine;

namespace Unity.Physics
{
    public class SnapToPos : SelfRegister<IInitializable<IGameContext>>, ISnapToPos, IInitializable<IGameContext>
    {
        [SerializeField] private int _priority;
        public int Priority => _priority;

        [SerializeField] private SerializedInterface<IGetClimbable> _getClimbableMono;
        public IGetClimbable GetClimbable => _getClimbableMono.Interface;

        private IPhysicsActor _actor;
        private IForceMoveActor _forceMoveActor;
        private IActorEventBus _actorEventBus;
        private void OnDestroy()
        {
            UnSubToEvents();
            base.OnDestroy();
        }
        public void Initialize(IGameContext context)
        {
            _forceMoveActor = context.PhysicsServices.ForceMoveActor;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_forceMoveActor != null, "Failed to initialize forceMoveActor");

            var provider = ProviderLookUp.Require<PlayerDataProvider>(this);
            _actorEventBus = provider.ActorEventBus;
            _actor = provider.Actor;

            SubToEvents();
        }
        private void HandleAction(IActionResult result)
        {
            if (result is ClimbResult climb)
            {
                IClimbable climbable = GetClimbable.FindClimbable(_actor.Body.RayConfig);
                SnapToPosition(climbable.Center);
            }
        }
        public void SnapToPosition(Vector2 pos)
        {
            _forceMoveActor.ForceMoveX(_actor, pos.x);
        }

        private void SubToEvents()
        {
            _actorEventBus.OnActionApproved += HandleAction;
        }

        private void UnSubToEvents()
        {
            _actorEventBus.OnActionApproved -= HandleAction;
        }

    }
}