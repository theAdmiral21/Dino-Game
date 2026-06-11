using System;
using System.Collections.Generic;
using Game.Core.State.Services;
using Movement.Core.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Movement.Abstractions;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Rules;
using Movement.Application;
using Movement.Application.Abstractions;
using Movement.Application.Dispatchers;
using Physics.Core.Abstractions;
using PlayerController.Application.Effects.Dispatchers;
using PlayerController.Application.Movement.Dispatchers;
using Physics.Core.DataStructures;
using Movement.Core.Stats;
using Primitives.Physics;
using Physics.Core.Buffers;
using Primitives.Stats.DataStructures;
using UnityEngine;
// using DG.Tweening.Core.Enums;
using System.Text;
using Physics.Core.PhysicsActors;

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

        private ActorActionContext _frameContext;

        // Cached Rule states
        private IJumpState _jumpState;
        private bool _hasJumpState;

        public ActorBrain(IPhysicsActor actor,
                            IActorInput actorInput,
                            IGameStateProvider gameState,
                            IRuleState ruleState,
                            IStatCollection stats,
                            RaycastConfiguration raycastConfig
                            )
        {
            _actor = actor;
            _gameState = gameState;
            _actorInput = actorInput;
            _actionDispatch = new ActionDispatcher(BuildActionDispatchers());
            _movementOrchestrator = new MovementOrchestrator(_actionDispatch);

            RaycastConfig = raycastConfig;

            // Make an event bus
            ActorEventBus = new ActorEventBus();
            Debug.Log($"Actor event bus configured");

            _ruleState = ruleState;
            // Register capabilities
            RegisterCapability(_ruleState);

            _stats = stats;
            KinematicResult kinematicState = new();

            FrameData = new ActorFrameData
            {
                DebugName = _actor.Name,
                Results = null,
                PhysicsContext = CurrentContext,
                CurrentState = kinematicState,
                ActorStats = _stats,
                RaycastConfig = RaycastConfig,
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
            _frameContext.Facts = CurrentContext;
            _frameContext.RuleState = _ruleState;

            // // Handle any buffered inputs
            DispatchBufferedJump();

            // Debug.Log($"Request count: {_frameContext.CurrentRequests.Count}");
            var results = Think(_frameContext);

            FrameData.Results = results;
            foreach (var res in results)
            {
                if (res.Approved)
                {
                    ActorEventBus.Publish(res);
                }

                if (res.ResultType == typeof(FallResult) && res.Approved)
                {
                    Debug.Log($"Approved fall for: {_actor.Name}; {_actor.GetComponent<Transform>().name}");
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
        };

            return dispatchers;
        }
    }
}