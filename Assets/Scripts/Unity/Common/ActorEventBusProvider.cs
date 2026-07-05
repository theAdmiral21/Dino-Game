using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Physics.Application.Orchestrators;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Unity.Common
{
    public class ActorEventBusProvider : SelfRegister<IInitializable<IGameContext>>, IActorEventBusProvider, IInitializable<IGameContext>
    {
        public IActorEventBus ActorEventBus => _actorEventBus;
        private IActorEventBus _actorEventBus;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        private void Awake()
        {
            base.Awake();
            _actorEventBus = new ActorEventBus();
        }

        public void Initialize(IGameContext context)
        {
            // throw new System.NotImplementedException();
        }

        public void PostInitialize(IGameContext context)
        {
            // throw new System.NotImplementedException();
        }
    }
}