using Core.Environment.Interactions;
using Core.Equipment;
using Core.Movement.Inputs;
using Core.Physics.PhysicsQueries;
using Game.Core.Execution;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Movement.Core.Stats;
using Physics.Application.DataStructures;
using Physics.Application.Orchestrators;
using Physics.Core.DataStructures;
using Primitives.Physics;
using Primitives.Physics.Enums;
using Unity.Common;
using Unity.Common.Unity;
using Unity.Infrastructure.Providers;
using UnityEngine;

namespace Physics.Unity.Actors
{
    public class PlayerActor : BasePhysicsActor,
                                IStunnable,
                                IKnockBackable,
                                IActionRequestSink
    {
        [SerializeField] private SerializedInterface<IGetClimbable> _getClimbableMono;
        private IGetClimbable _getClimbable => _getClimbableMono.Interface;

        [SerializeField] private SerializedInterface<ICheckCanStand> _crouchControllerMono;
        private ICheckCanStand _crouchController => _crouchControllerMono.Interface;

        // [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;
        IStatCollection _stats;

        // [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        IRuleState _ruleState;

        // [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        IActorInput _actorInput;

        [SerializeField] private SerializedInterface<IEquipmentBridge> _bridgeMono;
        IEquipmentBridge _equipmentBridge => _bridgeMono.Interface;

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

            switch (newRequest)
            {
                case ClimbRequest climb:
                    {
                        // check if there is something to climb
                        IClimbable climbable = _getClimbable.FindClimbable(Body.RayConfig);

                        // If you didn't find anything send None
                        if (climbable == null)
                        {
                            Brain.UpdateRequestList(climb);
                            return;
                        }

                        // Update the climb type
                        ClimbType climbType = climbable.GetClimbType(Body.RayConfig.Bounds.Center);
                        // Update the action with the correct data
                        climb = new ClimbRequest(climb.InputDir, climbType);

                        Brain.UpdateRequestList(climb);
                        return;
                    }
                case CrouchRequest crouch:
                    {
                        crouch = new CrouchRequest(_crouchController.CheckCanStand(Body.RayConfig));
                        Brain.UpdateRequestList(crouch);
                        return;
                    }
                case RunRequest run:
                    {
                        bool canStand = _crouchController.CheckCanStand(Body.RayConfig);
                        run = new RunRequest(run.BackUp, run.Value, run.CanStand);
                        Brain.UpdateRequestList(run);
                        break;
                    }
                default:
                    {
                        // Debug.Log($"Enqueuing {newRequest}");
                        Brain.UpdateRequestList(newRequest);
                        return;
                    }
            }
        }

        public override void Initialize(IGameContext context)
        {
            var provider = ProviderLookUp.Require<PlayerDataProvider>(this);
            _stats = provider.StatSheet.StatCollection;
            _ruleState = provider.RuleStateView;
            _actorInput = provider.ActorInput;
            var actorEventBus = provider.ActorEventBus;

            Debug.Assert(_ruleState != null, $"PlayerActor rule state is null");

            // Debug.Log($"PlayerActor.Initialize() - bounds: {_bounds} - Frame: {Time.frameCount}");
            // Debug.Log($"Initializing PlayerActor!");
            var rayConfig = new RaycastConfiguration(_bounds, _collisionLayer.value, gameObject.layer);

            // Add the body
            Body = new KinematicBody(_bounds, _transformProvider, rayConfig, _bodyType);

            // Add the brain
            Brain = new ActorBrain(
                this,
                _actorInput,
                context.GameStateServices.GameState,
                _ruleState,
                _stats,
                rayConfig,
                _equipmentBridge,
                actorEventBus);

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