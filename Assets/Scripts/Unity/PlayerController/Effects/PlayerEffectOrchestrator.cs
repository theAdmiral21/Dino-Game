using System.Collections.Generic;
using Game.Core.Effects;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using PlayerController.Application.Effects;
using PlayerController.Application.Effects.Abstractions;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Unity.Physics;
using Primitives.Physics;
using Primitives.Stats;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.Effects
{
    public class PlayerEffectOrchestrator : SelfRegister<IInitializable<IGameContext>>, IPlayerEffectOrchestrator, IInitializable<IGameContext>
    {
        [SerializeField] private EffectDriver _effectDriver;
        private List<IEffectResult> _effectResults = new List<IEffectResult>();
        private PhysicsContext _physicsContext;
        private IRuleState _ruleState;
        private StepDetector _stepDetector;

        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        IActorEventBus _actorEventBus => _actorEventBusMono.Interface.ActorEventBus;

        [SerializeField] private SerializedInterface<IStateEffect> _visualStateEffectsMono;
        IStateEffect _visualStateEffects => _visualStateEffectsMono.Interface;

        [SerializeField] private SerializedInterface<IStateEffect> _audioStateEffectsMono;
        IStateEffect _audioStateEffects => _audioStateEffectsMono.Interface;

        [SerializeField] private float _stepDistance;

        public int Priority => 0;

        private new void OnDestroy()
        {
            base.OnDestroy();
            _actorEventBus.OnActionApproved -= EvaluateActionEffects;
        }
        public void SetPhysicsContext(PhysicsContext physicsContext)
        {
            _physicsContext = physicsContext;
        }

        public void SetRuleState(IRuleState playerRuleState)
        {
            _ruleState = playerRuleState;
        }

        public void SetRunValues(float runSpeed, float sprintSpeed)
        {
            _stepDetector = new StepDetector(runSpeed, sprintSpeed);
        }

        public void EvaluateActionEffects(IActionResult actionResult)
        {
            if (!actionResult.Approved) return;

            // Map the approved actions to their effects
            switch (actionResult)
            {

                case JumpResult jump:
                    {
                        if (jump.Type == JumpType.WallJump)
                        {
                            _effectResults.Add(new WallJumpEffect(true, _physicsContext.Surface));

                        }
                        else if (jump.Type == JumpType.Double)
                        {
                            // Debug.Log($"Adding double jump effect");
                            _effectResults.Add(new DoubleJumpEffect(true));
                        }
                        else
                        {
                            _effectResults.Add(new JumpEffect(true, _physicsContext.Surface));
                        }
                        break;
                    }
                case RunResult run:
                    {
                        StepEffect effect = _stepDetector.TryStep(_physicsContext, _stepDistance);
                        _effectResults.Add(effect);
                        // Debug.Log($"Adding step effect");
                        _effectResults.Add(new RunEffect(true, run.Value.x));
                        break;
                    }
                case DodgeResult bark:
                    {
                        // Debug.Log("Got bark effect");
                        _effectResults.Add(new DodgeEffect(true));
                        break;
                    }
                case LandingResult land:
                    {
                        // Debug.Log($"Got landing effect");
                        _effectResults.Add(new LandEffect(true, land.Surface));
                        break;
                    }
                case ScentResult scent:
                    {
                        Debug.Log($"Got scent effect");
                        _effectResults.Add(new ScentEffect(true));
                        break;
                    }
            }
        }
        public void EvaluateStateEffects(IRuleState actorRuleState)
        {
            _effectResults.AddRange(_visualStateEffects.EvaluateStateEffects(actorRuleState, _physicsContext));

            _effectResults.AddRange(_audioStateEffects.EvaluateStateEffects(actorRuleState, _physicsContext));

            // actorRuleState.TryGet<IInvincibleState>(out var invincible);
            // actorRuleState.TryGet<IStunState>(out var stun);
            // actorRuleState.TryGet<IZoomiesState>(out var zoomies);
            // if (invincible.IsInvincible || stun.IsStunned)
            // {
            //     _effectResults.Add(new IFrameEffect(true));
            // }
            // else
            // {
            //     _effectResults.Add(new IFrameEffect(false));
            // }

            // // Evaluate Zoomies effect
            // if (zoomies.IsZooming)
            // {
            //     _effectResults.Add(new ZoomiesEffect(true));
            // }
            // else
            // {
            //     _effectResults.Add(new ZoomiesEffect(false));
            // }

            // // Evaluate physics effects
            // _effectResults.Add(new WallSlideEffect(_physicsContext.IsWallSliding, _physicsContext.Surface));


            _effectDriver.EnqueueEffectResults(_effectResults);
            _effectResults.Clear();
        }

        public void Initialize(IGameContext context)
        {
            _actorEventBus.OnActionApproved += EvaluateActionEffects;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_actorEventBus != null, "Unable to get ActorEventBus");
        }


    }
}