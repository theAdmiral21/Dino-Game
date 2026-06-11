using Core.Movement.Abstractions;
using Game.Core.Execution;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using Physics.Application.DataStructures;
using Physics.Application.Orchestrators;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Physics.Core.PhysicsQueries;
using Physics.Unity.ContextBuilders;
using Primitives.Physics;
using Unity.Common.Unity;
using UnityEditor.Build;
using UnityEngine;

namespace Physics.Unity.Actors
{
    public class PlayerActor : BasePhysicsActor,
                                IStunnable,
                                IKnockBackable,
                                IActionRequestSink
    {
        [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;
        IStatCollection _stats => _statProviderMono.Interface.StatSheet.StatCollection;

        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        IRuleState _ruleState => _ruleStateMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        IActorInput _actorInput => _actorInputMono.Interface;

        private BodyType _bodyType = BodyType.Kinematic;
        // private IJumpContextBuilder _jumpContextBuilder;
        private IDirectionState _dirState;

        [Header("Debug")]
        [SerializeField] private KinematicResult _debugState;
        [SerializeField] private PhysicsContext _debugPhysics;

        protected override void Awake()
        {
            base.Awake();

            // _jumpContextBuilder = new JumpContextBuilder(_stats);

            Debug.Log($"player bounds: {_bounds}");

        }
        protected override void OnDestroy()
        {
            Debug.Log($"Destroying player actor - frame: {Time.frameCount}");
            base.OnDestroy();
        }
        public override void EnqueueActionRequest(IActionRequest newRequest)
        {
            if (Brain == null) return;

            // switch (newRequest)
            // {
            // case JumpRequest jump:
            //     {
            //         // Update the jump request with the jump context
            //         jump.Context = _jumpContextBuilder.BuildJumpContext(
            //             Body.RayConfig,
            //             Brain.FrameData.CurrentState.Velocity,
            //             Brain.FrameData.CurrentState.Gravity,
            //             _dirState.Dir
            //         );
            //         // Debug.Log($"Adding new jump request. Normal: {jump.Context.HitNormal}Contact: {jump.Context.MadeContact}");
            //         Brain.UpdateRequestList(jump);
            //         return;
            //     }
            // default:
            //     {
            // Debug.Log($"Enqueuing {newRequest}");
            Brain.UpdateRequestList(newRequest);
            // return;
            // }
            // }
        }

        public override void Initialize(IGameContext context)
        {
            Debug.Log($"PlayerActor.Initialize() - bounds: {_bounds} - Frame: {Time.frameCount}");
            // Debug.Log($"Initializing PlayerActor!");
            var rayConfig = new RaycastConfiguration(_bounds, _collisionLayer.value);

            // Add the body
            Body = new KinematicBody(_bounds, _transformProvider, rayConfig, _bodyType);

            // Add the brain
            Brain = new ActorBrain(
                this,
                _actorInput,
                context.GameStateServices.GameState,
                _ruleState,
                _stats,
                rayConfig);

            Debug.Assert(_ruleState.TryGet<IDirectionState>(out var dir), $"Failed to get direction state from rule state");
            _dirState = dir;
        }

        public override void PostInitialize(IGameContext context)
        {
            Debug.Assert(Body != null, "Failed to initialize actor body.");
            Debug.Assert(Brain != null, "Failed to initialize actor brain.");
        }

        private void LateUpdate()
        {
            _debugState = Brain.KinematicState;
            _debugPhysics = Brain.CurrentContext;
        }

        public void Stun(float duration)
        {
            Debug.Log("Adding stun request");
            EnqueueActionRequest(new StunRequest(true, duration));
        }
        public void KnockBack(float apexTime, Vector2 velocity)
        {
            Debug.Log("Adding knock back request");
            EnqueueActionRequest(new KnockBackRequest(true, apexTime, velocity));
        }
    }
}