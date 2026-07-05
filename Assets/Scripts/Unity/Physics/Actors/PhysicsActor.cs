using System.Collections.Generic;
using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Game.Core.Execution;
using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using Physics.Application.DataStructures;
using Physics.Application.Orchestrators;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using Unity.Common;
using Unity.Common.Unity;
using Unity.Infrastructure.Providers;
using UnityEngine;

namespace Physics.Unity.Actors
{
    [RequireComponent(typeof(Collider2D))]
    public class PhysicsActor : BasePhysicsActor,
                                IActionRequestSink,
                                IActionResultViewer

    {

        public RaycastConfiguration RayConfig { get; private set; }
        public BodyType BodyType => _bodyType;
        [SerializeField] BodyType _bodyType;



        public List<IActionRequest> ActionRequests => Brain.ActionRequests;

        // [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;
        IStatCollection _stats;
        // [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        IRuleState _ruleState;

        // [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        IActorInput _actorInput;
        public List<IActionResult> ActionResults => Brain.ActionResults;



        [Header("Debug")]
        [SerializeField] private KinematicResult _debugState;
        [SerializeField] private PhysicsContext _debugPhysics;

        public override void Initialize(IGameContext context)
        {
            // var provider = ProviderLookUp.Require<PlayerDataProvider>(this);
            _stats = ProviderLookUp.Require<IStatProvider>(this).StatSheet.StatCollection;
            _ruleState = ProviderLookUp.Require<IRuleStateProvider>(this).RuleStateView;
            _actorInput = ProviderLookUp.Require<IActorInputContext>(this).ActorInput;
            var actorEventBus = ProviderLookUp.Require<IActorEventBusProvider>(this).ActorEventBus;

            Debug.Log($"{Name} has ruleState: {_ruleState != null}");

            RayConfig = new RaycastConfiguration(_bounds, _collisionLayer.value, gameObject.layer);
            // Add the body
            Body = new KinematicBody(_bounds, _transformProvider, RayConfig, _bodyType);

            // Add the brain
            if (_bodyType == BodyType.Static)
            {
                Brain = new ActorBrain(
                    this,
                    null,
                    null,
                    null,
                    null,
                    RayConfig,
                    null,
                    actorEventBus);
            }
            else
            {
                Brain = new ActorBrain(
                    this,
                    _actorInput,
                    context.GameStateServices.GameState,
                    _ruleState,
                    _stats,
                    RayConfig,
                    null,
                    actorEventBus);
            }
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