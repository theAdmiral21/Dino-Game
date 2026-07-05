using System.Collections.Generic;
using Core.Movement.Abstractions;
using Game.Core.Effects;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using Unity.Common.Unity;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public abstract class BaseEffectTranslator : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>, IEffectTranslator
    {
        protected List<IEffectResult> _effectResults = new List<IEffectResult>();

        [SerializeField] private SerializedInterface<IPhysicsActor> _physicsActorMono;
        protected IActionResultViewer _resultViewer => _physicsActorMono.Interface.Brain;
        protected PhysicsContext _physicsContext => _physicsActorMono.Interface.Brain.CurrentContext;

        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        protected IActorEventBus _actorEventBus => _actorEventBusMono.Interface.ActorEventBus;

        [SerializeField] private SerializedInterface<IStateEffect> _visualStateEffectsMono;
        protected IStateEffect _visualStateEffects => _visualStateEffectsMono.Interface;

        [SerializeField] private SerializedInterface<IStateEffect> _audioStateEffectsMono;
        protected IStateEffect _audioStateEffects => _audioStateEffectsMono.Interface;

        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateViewMono;
        private IRuleState _ruleStateView => _ruleStateViewMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IEffectOrchestrator> _effectOrchestratorMono;
        protected IEffectOrchestrator _effectOrchestrator => _effectOrchestratorMono.Interface;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;

        protected void OnDestroy()
        {
            _actorEventBus.OnActionApproved -= ConvertActionEffects;
            base.OnDestroy();
        }

        public void Initialize(IGameContext context)
        {
            // The physics actor needs to initialize before we can get the event but.
        }

        public void PostInitialize(IGameContext context)
        {
            _actorEventBus.OnActionApproved += ConvertActionEffects;
            Debug.Assert(_actorEventBus != null, "Unable to get ActorEventBus");
        }

        public abstract void ConvertActionEffects(IActionResult effect);

        public void EvaluateStateEffects(IRuleState ruleState)
        {
            // Not everything will have state effects. So if a script for the state effects isn't assigned, ignore these.
            if (_visualStateEffects != null)
            {
                _effectResults.AddRange(_visualStateEffects.EvaluateStateEffects(ruleState, _physicsContext));
            }

            if (_audioStateEffects != null)
            {
                _effectResults.AddRange(_audioStateEffects.EvaluateStateEffects(ruleState, _physicsContext));
            }
        }

        private void Update()
        {
            EvaluateStateEffects(_ruleStateView);
            _effectOrchestrator.EnqueueEffectResults(_effectResults);
            _effectResults.Clear();
        }


    }
}