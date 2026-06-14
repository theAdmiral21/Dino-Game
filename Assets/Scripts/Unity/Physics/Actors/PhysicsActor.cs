using System.Collections.Generic;
using Core.Movement.Abstractions;
using Game.Core.Execution;
using Game.Core.State.Services;
using Gameplay.Common.Unity;

using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.DataStructures;
using Movement.Core.Inputs;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using Physics.Application.DataStructures;
using Physics.Application.Orchestrators;
using Physics.Core.Abstractions;
using Physics.Core.Buffers;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Physics.Core.PhysicsQueries;
using Physics.Unity.ContextBuilders;
using Physics.Unity.Physics;
using Primitives.Physics;
using Primitives.Stats.DataStructures;
using Unity.Common.Unity;

using UnityEngine;

namespace Physics.Unity.Actors
{
    [RequireComponent(typeof(Collider2D))]
    public class PhysicsActor : BasePhysicsActor,
                                //SelfRegister<IInitializable<IGameContext>>,
                                // IPhysicsActor,
                                // IExternalForceReceiver,
                                // IInitializable<IGameContext>,
                                IActionRequestSink,
                                IActionResultViewer
    // IStunnable,
    // IKnockBackable
    // IActorEventBusProvider
    {
        // public IActorBrain Brain { get; private set; }
        // public IKinematicBody Body { get; private set; }
        public RaycastConfiguration RayConfig { get; private set; }
        public BodyType BodyType => _bodyType;
        [SerializeField] BodyType _bodyType;
        // public bool IsAsleep { get; private set; }
        // public ActorType Actor => _actor;
        // [SerializeField] private ActorType _actor;
        // public Vector2 MoveVector { get; set; }

        // public string Name => _name;

        // public IBoundsProvider Bounds => _bounds;
        // private IBoundsProvider _bounds;

        // public ITransformProvider TransformProvider => _transformProvider;
        // private ITransformProvider _transformProvider;


        // public bool ReadyToDestroy => _readyToDestroy;
        // private bool _readyToDestroy;

        // public int Priority => 5;

        public List<IActionRequest> ActionRequests => Brain.ActionRequests;


        // [SerializeField] private string _name;

        // [SerializeField] private SerializedInterface<IActionBuffer> _actionBufferMono;
        // private IActionBuffer _actionBuffer => _actionBufferMono.Interface;

        [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;
        IStatCollection _stats => _statProviderMono.Interface.StatSheet.StatCollection;
        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        IRuleState _ruleState => _ruleStateMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        IActorInput _actorInput => _actorInputMono.Interface;
        public List<IActionResult> ActionResults => Brain.ActionResults;
        // public IActorEventBus ActorEventBus => Brain.ActorEventBus;
        // private IGameStateProvider _gameState;
        // private LayerMask _collisionLayer;



        [Header("Debug")]
        [SerializeField] private KinematicResult _debugState;
        [SerializeField] private PhysicsContext _debugPhysics;


        // private new void Awake()
        // {
        //     base.Awake();
        //     RegistryGateway.Register<IPhysicsActor>(this);

        //     Collider2D collider = GetComponent<Collider2D>();
        //     _bounds = new UnityColliderBoundsProvider(collider);
        //     _transformProvider = new UnityTransformProvider(transform);

        //     // The collision layer should always be collision
        //     _collisionLayer = LayerMask.GetMask("Collision");
        //     Debug.Assert(_collisionLayer.value == (1 << 7), $"Collision layer mask is using layer {_collisionLayer.value}");
        // }



        public override void Initialize(IGameContext context)
        {
            var rayConfig = new RaycastConfiguration(_bounds, _collisionLayer.value);

            // Add the body
            Body = new KinematicBody(_bounds, _transformProvider, RayConfig, _bodyType);

            // Add the brain
            Brain = new ActorBrain(
                this,
                _actorInput,
                context.GameStateServices.GameState,
                _ruleState,
                _stats,
                rayConfig,
                null);
        }

        public override void PostInitialize(IGameContext context)
        {
            Debug.Assert(Brain != null, "Failed to initialize actor brain.");
        }

        private void LateUpdate()
        {
            _debugState = Brain.KinematicState;
            _debugPhysics = Brain.CurrentContext;
        }

        // public void ReceiveImpulse(IActionRequest request)
        // {
        //     // Debug.Log("Adding External Impulse request");
        //     // ActionResults.Add(result);

        //     Brain.UpdateRequestList(request);
        // }

        // public void ReceiveContinuous(IActionRequest request)
        // {
        //     // Debug.Log("Adding External Continuous request");
        //     // ActionResults.Add(result);

        //     Debug.LogError($"Reimplement external continuous forces");
        //     Brain.UpdateRequestList(request);
        // }



        // public void MarkForDestruction()
        // {
        //     if (!_readyToDestroy)
        //     {
        //         _readyToDestroy = true;
        //     }
        // }

        // public void Destruct()
        // {
        //     if (_readyToDestroy)
        //     {
        //         Destroy(gameObject);
        //     }
        // }

        // public void Sleep()
        // {
        //     // Sleep the player -> Stops updates
        //     IsAsleep = true;
        //     // Clear any forces acting on the actor -> zeros any movement
        //     Brain.KinematicState.ClearForces();
        //     // Clear any action results -> Clears any approved future movement
        //     if (Brain.ActionResults != null)
        //     {
        //         Brain.ActionResults.Clear();
        //     }
        // }

        // public void WakeUp()
        // {
        //     // wake up the player -> Restart updates
        //     IsAsleep = false;
        //     // Clear any forces acting on the actor -> zeros any movement while disabled
        //     Brain.KinematicState.ClearForces();
        //     // Clear any action results -> Clears any approved movement accrued while asleep
        //     if (Brain.ActionResults != null)
        //     {
        //         Brain.ActionResults.Clear();
        //     }
        // }

        public override void EnqueueActionRequest(IActionRequest newRequest)
        {

            // Debug.Log($"{Name} | InstanceID: {GetInstanceID()} | Brain null: {Brain == null}");
            if (Brain == null) return;

            // switch (newRequest)
            // {
            //     case JumpRequest jump:
            //         {

            //             // Update the jump request with the jump context
            //             jump.Context = _jumpContextBuilder.BuildJumpContext(
            //                 RayConfig,
            //                 Brain.FrameData.CurrentState.Velocity,
            //                 Brain.FrameData.CurrentState.Gravity,
            //                 _dirState.Dir
            //             );
            //             // Debug.Log($"Adding new jump request. Normal: {jump.Context.HitNormal}Contact: {jump.Context.MadeContact}");
            //             Brain.UpdateRequestList(jump);
            //             return;
            //         }
            //     default:
            //         {
            // Debug.Log($"Enqueuing {newRequest}");
            Brain.UpdateRequestList(newRequest);
            //             return;
            //         }
            // }

        }

        // public void Stun(float duration)
        // {
        //     Debug.Log("Adding stun request");
        //     Brain.UpdateRequestList(new StunRequest(true, duration));
        // }
        // public void KnockBack(float apexTime, Vector2 velocity)
        // {
        //     Debug.Log("Adding knock back request");
        //     Brain.UpdateRequestList(new KnockBackRequest(true, apexTime, velocity));
        // }
    }
}