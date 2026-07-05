using System;
using System.Collections.Generic;
using Game.Core.State.Services;
using Movement.Core.Abstractions;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Movement.Application;
using Movement.Application.Abstractions;
using Movement.Application.Dispatchers;
using PlayerController.Application.Movement.Dispatchers;
using Physics.Core.DataStructures;
using Movement.Core.Stats;
using Primitives.Physics;
// using DG.Tweening.Core.Enums;
using Physics.Core.PhysicsActors;
using Core.Equipment;
using Core.Movement.Inputs;
using UnityEngine;

namespace Physics.Application.Orchestrators
{
    public class ActorBrain : IActorBrain
    {
        private readonly IPhysicsActor _actor;
        private ActionDispatcher _actionDispatch;
        private MovementOrchestrator _movementOrchestrator;
        public List<IActionResult> ActionResults => FrameData.Results;
        public List<IActionRequest> ActionRequests { get; private set; } = new();
        public IActorEventBus ActorEventBus { get; private set; }
        public RaycastConfiguration RaycastConfig { get; private set; }
        public ActorFrameData FrameData { get; set; }

        public PhysicsContext CurrentContext { get; set; } = new();
        public KinematicResult KinematicState => FrameData.CurrentState;
        // private KinematicResult _kinematicState;
        private IRuleState _ruleState;
        private IGameStateProvider _gameState;
        private IActorInput _actorInput;
        private Dictionary<Type, object> _capabilities = new();
        private IStatCollection _stats;
        private IEquipmentBridge _equipmentBridge;
        private ActorActionContext _frameContext;

        // Cached Rule states
        private IJumpState _jumpState;
        private bool _hasJumpState;

        public ActorBrain(IPhysicsActor actor,
                            IActorInput actorInput,
                            IGameStateProvider gameState,
                            IRuleState ruleState,
                            IStatCollection stats,
                            RaycastConfiguration raycastConfig,
                            IEquipmentBridge equipmentBridge,
                            IActorEventBus actorEventBus
                            )
        {
            _actor = actor;
            _gameState = gameState;
            _actorInput = actorInput;
            _actionDispatch = new ActionDispatcher(BuildActionDispatchers());
            _movementOrchestrator = new MovementOrchestrator(_actionDispatch);
            ActorEventBus = actorEventBus;
            RaycastConfig = raycastConfig;
            _equipmentBridge = equipmentBridge;

            Debug.Assert(_actor != null, $"actor is null!");
            Debug.Assert(_gameState != null, $"gameState is null!");
            Debug.Assert(_actorInput != null, $"actorInput is null!");
            Debug.Assert(ruleState != null, $"ruleState is null for {_actor.Name}!");
            Debug.Assert(stats != null, $"stats is null!");
            Debug.Assert(raycastConfig != null, $"raycastConfig is null for {_actor.Name}!");
            Debug.Assert(equipmentBridge != null, $"equipmentBridge is null for {_actor.Name}!");
            Debug.Assert(actorEventBus != null, $"ActorEventBus is null for {_actor.Name}!");

            _ruleState = ruleState;

            Debug.Log($"actor {_actor.Name} has ruleState: {_ruleState != null}");

            // Register capabilities
            if (_actor.Body.BodyType == BodyType.Kinematic)
            {
                RegisterCapability(_ruleState);
                _stats = stats;
            }

            KinematicResult kinematicState = new();

            FrameData = new ActorFrameData
            {
                DebugName = _actor.Name,
                Results = null,
                PhysicsContext = CurrentContext,
                CurrentState = kinematicState,
                ActorStats = _stats,
                RaycastConfig = RaycastConfig,
                RayCollisions = new(),
            };

            CacheStatVals();
        }

        private void CacheStatVals()
        {
            _hasJumpState = _ruleState.TryGet<IJumpState>(out var jumpState);
            if (_hasJumpState) _jumpState = jumpState;

        }


        public List<IActionResult> Think(ActorActionContext context)
        {
            // StringBuilder debugSb = new StringBuilder();
            // debugSb.Append("Requests:");
            // foreach (var request in context.CurrentRequests)
            // {
            //     debugSb.AppendLine($"{request}");
            // }
            // Debug.Log(debugSb);
            return _movementOrchestrator.ProcessActions(context);
        }

        public void Tick(float dt)
        {
            if (_actor.Body.BodyType == BodyType.Static) return;

            // Debug.Log($"Request count: {ActionRequests.Count}");
            _frameContext = new ActorActionContext
            {
                CurrentRequests = ActionRequests,
                // Facts = CurrentContext,
                // RuleState = _ruleState,
                CurrentGameState = _gameState.CurrentState,
                InputValues = _actorInput,
                Dt = dt,
            };
        }

        public void ResolveRequests()
        {
            if (_actor.Body.BodyType == BodyType.Static) return;

            _frameContext.Facts = CurrentContext;
            _frameContext.RuleState = _ruleState;

            // // Handle any buffered inputs
            DispatchBufferedJump();

            // Debug.Log($"Request count: {_frameContext.CurrentRequests.Count}");
            var results = Think(_frameContext);

            FrameData.Results = results;
            foreach (var res in results)
            {
                // Debug.Log($"result: {res}; approved: {res.Approved}");
                // // Check if you can cast the result to an equipment result
                // if (res is IEquipmentActionResult)
                // {
                //     Debug.Log($"Got equipment result!");
                //     _equipmentBridge.RouteEquipmentResult(res as IEquipmentActionResult);
                // }

                if (res.Approved)
                {
                    if (res is IEquipmentActionResult)
                    {
                        ActorEventBus.Publish(res as IEquipmentActionResult);
                    }
                    else
                    {
                        ActorEventBus.Publish(res);
                    }
                }
            }
            // Clear action requests
            ClearRequestList();
        }

        public void UpdateRequestList(IActionRequest newRequest)
        {
            // var request = BufferInputs(newRequest);
            // if (request != null)
            // {
            // if (request is JumpRequest)
            // {
            //     JumpRequest jump = (JumpRequest)request;
            //     Debug.Log($"Buffering jump request of type: {jump.JumpType}");
            // }
            ActionRequests.Add(newRequest);
            // }
        }

        public void ClearRequestList()
        {
            ActionRequests.Clear();
        }

        public void RegisterCapability(object capability)
        {
            // Get the interfaces implemented in the rule state
            var interfaces = capability.GetType().GetInterfaces();
            foreach (var i in interfaces)
            {
                // Debug.Log($"Adding capability: {capability} with interface {i}");
                _capabilities[i] = capability;
            }
        }
        public T GetCapability<T>() where T : class
        {
            if (_capabilities.TryGetValue(typeof(T), out var capability))
            {
                return capability as T;
            }
            return null;
        }


        private void DispatchBufferedJump()
        {

            if (_hasJumpState)
            {
                if (_jumpState.JumpBuffered)
                {
                    // Debug.Log($"Buffered jump dispatched - frame {Time.frameCount}");
                    ActionRequests.Add(new JumpRequest(true, JumpType.Ground));
                }
            }
        }

        private IDispatchRequest[] BuildActionDispatchers()
        {
            var dispatchers = new IDispatchRequest[]
            {
                new JumpDispatcher(),
                new JumpCancelDispatcher(),
                new RunDispatcher(),
                new PauseDispatcher(),
                new StunDispatcher(),
                new KnockBackDispatcher(),
                new FlyDispatcher(),
                new ExternalContinuousDispatcher(),
                new ExternalImpulseDispatcher(),
                new DodgeDispatcher(),
                new CrouchDispatcher(),
                new RaiseWeaponDispatcher(),
                new ShootDispatcher(),
                new AimDispatcher(),
                new ReloadDispatcher(),
                new LungeDispatcher(),
        };

            return dispatchers;
        }
    }
}