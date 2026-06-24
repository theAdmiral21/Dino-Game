using System.Collections.Generic;
using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Game.Core.Animations;
using Game.Core.Audio;
using Game.Core.Effects;
using Movement.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using Unity.Common.Unity;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public class NpcEffectOrchestrator : MonoBehaviour, IEffectOrchestrator
    {
        [SerializeField] private SerializedInterface<IAnimatorBridge> _animatorBridgeMono;
        protected IAnimatorBridge _animatorBridge => _animatorBridgeMono.Interface;
        [SerializeField] private SerializedInterface<IParticleBridge> _particleBridgeMono;
        protected IParticleBridge _particleBridge => _particleBridgeMono.Interface;

        [SerializeField] private SerializedInterface<IAudioBridge> _audioBridgeMono;
        protected IAudioBridge _audioBridge => _audioBridgeMono.Interface;

        [SerializeField] private SerializedInterface<IPhysicsActor> _physicsActorMono;
        protected PhysicsContext _physicsContext => _physicsActorMono.Interface.Brain.CurrentContext;

        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateViewMono;
        protected IRuleState _ruleStateView => _ruleStateViewMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        protected IActorInput _actorInput => _actorInputMono.Interface;

        private List<IEffectResult> _queuedEffects = new List<IEffectResult>();

        public void EnqueueEffectResults(List<IEffectResult> effectResults)
        {
            if (effectResults == null) return;
            _queuedEffects.AddRange(effectResults);
        }

        private void Update()
        {
            // Apply the effects
            var effectResults = _queuedEffects;

            // Do your stuff
            foreach (var effect in effectResults)
            {
                HandleAudio(effect);
                HandleVisual(effect);
                HandleParticle(effect);
            }

            _animatorBridge.SyncAnimation(_actorInput, _physicsContext, _ruleStateView);

            // Clear the list
            _queuedEffects.Clear();
        }

        public void HandleAudio(IEffectResult audioEffect)
        {
            _audioBridge.HandleSound(audioEffect);
        }

        // Can this just be moved to the base class or would that be too confusing in future implementations? <.<
        public void HandleVisual(IEffectResult visualEffect)
        {
            _animatorBridge.ApplyEffect(visualEffect);
        }

        public void HandleParticle(IEffectResult visualEffect)
        {
            _particleBridge.ApplyEffect(visualEffect);
        }
    }
}