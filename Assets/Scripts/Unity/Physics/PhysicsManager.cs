using System.Collections.Generic;
using System.Linq;
using Gameplay.Common.Application.Abstractions;
using Gameplay.Common.Application.DataStructures;
using Gameplay.Common.Core.Abstractions;
using Infrastructure.Unity.Lifecycle;
using Infrastructure.Unity.Registries;
using Physics.Application.Abstractions;
using Physics.Application.Collisions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using Primitives.Physics.DataStructures;
using Unity.Common.Unity;
using UnityEngine;

namespace Physics.Unity.Physics
{
    public class PhysicsManager : MonoBehaviour
    {
        [SerializeField] private EntityDestroyer _entityDestroyer;

        [SerializeField] private MonoBehaviour _simDriverMono;
        private ISimulationDriver _simDriver;

        [SerializeField] private MonoBehaviour _physicsRegistryMono;
        private IPhysicsRegistry _physicsRegistry;

        public IReadOnlyCollection<IPhysicsActor> ActorRegistry => _physicsRegistry.Actors;
        public IReadOnlyCollection<ITriggerVolume> TriggerRegistry => _physicsRegistry.Triggers;

        [SerializeField] private SerializedInterface<IMoveActor> _actorMoverMono;
        private IMoveActor _actorMover => _actorMoverMono.Interface;

        [SerializeField] private SerializedInterface<IPhysicsMonitor> _physicsMonitorMono;
        private IPhysicsMonitor _physicsMonitor => _physicsMonitorMono.Interface;

        private HashSet<OverlapPair> _previousTriggerPairs = new();
        private HashSet<OverlapPair> _currentTriggerPairs = new();

        private IDetectCollision _collisionDetection;

        public static PhysicsManager Instance { get; private set; }

        // private StringBuilder _debugSb = new();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _simDriver = _simDriverMono as ISimulationDriver;
            if (_simDriver == null)
            {
                Debug.LogError($"Unable to convert {_simDriverMono.name} to ISimulationDriver.");
                return;
            }

            _physicsRegistry = _physicsRegistryMono as IPhysicsRegistry;
            if (_physicsRegistry == null)
            {
                Debug.LogError($"Unable to convert {_physicsRegistryMono.name} to IPhysicsRegistry.");
                return;
            }

            // Add the collision detector
            _collisionDetection = new CollisionDetection();

            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED PhysicsManager {GetInstanceID()}");
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED PhysicsManager {GetInstanceID()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            // Update intent
            // NOTE eventually I need to get rid of all of these foreach loops and use just for loops. 
            foreach (IPhysicsActor actor in ActorRegistry)
            // for (int i = 0; i < ActorRegistry.Count; i ++)
            {
                if (actor.IsAsleep) continue;
                // Gather requests
                actor.Brain.Tick(dt);
            }

            // // Update physics context
            // foreach (var actor in ActorRegistry)
            // {
            //     if (actor.IsAsleep) continue;

            //     _physicsMonitor.UpdatePhysicsContext(
            //         actor,
            //         actor.Brain.FrameData.CurrentState,
            //         actor.Brain.FrameData.PhysicsContext,
            //         actor.Brain.FrameData.RaycastConfig);
            // }

            // Move the platforms first
            foreach (var actor in ActorRegistry.Where(a => a.Actor == ActorType.Platform))
            {
                if (actor.IsAsleep) continue;

                _simDriver.Step(actor.Brain.FrameData, dt);

                MoveActor(actor);

            }

            // Update non platform's context
            foreach (var actor in ActorRegistry.Where(a => a.Actor != ActorType.Platform))
            {

                _simDriver.Step(actor.Brain.FrameData, dt);

                if (actor.IsAsleep) continue;

                MoveActor(actor);
            }

            // Check for collision?
            HandleCollisions(3);

            // Update physics context
            foreach (var actor in ActorRegistry)
            {
                if (actor.IsAsleep) continue;

                _physicsMonitor.UpdatePhysicsContext(
                    actor,
                    actor.Brain.FrameData.CurrentState,
                    actor.Brain.FrameData.PhysicsContext,
                    actor.Brain.FrameData.RaycastConfig);
            }

            foreach (var actor in ActorRegistry)
            {
                if (actor.IsAsleep) continue;
                actor.Brain.ResolveRequests();
            }

            foreach (IPhysicsActor actor in ActorRegistry)
            {
                // Add destroyed actors to the destroy queue
                if (actor.ReadyToDestroy)
                {
                    _entityDestroyer.AddToDestroyQueue(actor);
                }
            }

            // Check for trigger overlap
            ResolveTriggers();

        }

        /// <summary>
        /// Iterates through and resolves all the collisions in the current frame. Note that resolving collisions can lead to more collisions. Increasing iterations increases stability and increases frame time leading to lag. 
        /// </summary>
        /// <param name="iterations"></param>
        private void HandleCollisions(int iterations = 1)
        {
            for (int i = 0; i < iterations; i++)
            {
                var resolveDict = _collisionDetection.GetCollisions(ActorRegistry.ToList());

                // If nothing had to be resolved we're already stable
                if (resolveDict.Count == 0) break;

                foreach (var actor in resolveDict.Keys.ToList())
                {
                    // Debug.Log($"Resolving {actor.Name} {resolveList[actor]}");
                    MoveActor(actor, resolveDict[actor]);

                    // Reset the actor's velocity on contact
                    Vector2 sep = resolveDict[actor];
                    if (Mathf.Abs(sep.x) > Mathf.Abs(sep.y))
                    {
                        // Horizontal collision
                        actor.Brain.FrameData.CurrentState.Velocity.x = 0;
                    }
                    else
                    {
                        // Vertical collision
                        actor.Brain.FrameData.CurrentState.Velocity.y = 0;
                    }
                }
            }
        }

        private void MoveActor(IPhysicsActor actor)
        {
            // Nudge the player?
            // Debug.Log($"actor type: {actor.GetType()}");
            _actorMover.MoveActor(actor, actor.Brain.FrameData.CurrentState.CornerNudge);
            // Man this is a mouth full
            _actorMover.MoveActor(actor, actor.Brain.FrameData.CurrentState.FrameDelta);
            _actorMover.RotateActor(actor, actor.Brain.FrameData.CurrentState.AngularFrameDelta);
        }

        private void MoveActor(IPhysicsActor actor, Vector2 velocity)
        {
            // Man this is a mouth full
            _actorMover.MoveActor(actor, velocity);
            _actorMover.RotateActor(actor, actor.Brain.FrameData.CurrentState.AngularFrameDelta);
        }

        private void ResolveTriggers()
        {
            // Clear the current trigger pairs
            _currentTriggerPairs.Clear();

            foreach (IPhysicsActor actor in ActorRegistry)
            {
                AABB actorGeometry = actor.Body.Bounds.GetBounds();
                foreach (ITriggerVolume trigger in TriggerRegistry)
                {
                    AABB triggerGeometry = trigger.BoundsProvider.GetBounds();
                    if (actorGeometry.Intersects(triggerGeometry))
                    {
                        // Debug.Log($"{actor} overlaps {trigger}");
                        _currentTriggerPairs.Add(new OverlapPair(actor, trigger));
                    }
                }
            }
            DiffAndDispatch();
            // Make sure to update the previous trigger properly or things wont work
            _previousTriggerPairs = new HashSet<OverlapPair>(_currentTriggerPairs);
        }

        private void DiffAndDispatch()
        {
            // Enter and Stay
            foreach (OverlapPair pair in _currentTriggerPairs)
            {
                // Debug.Log($"Checking pair containing {pair.Actor} for enter/stay");
                // Debug.Log($"Is enter: {!_previousTriggerPairs.Contains(pair)}");
                // Debug.Log($"Is stay: {_previousTriggerPairs.Contains(pair)}");
                if (!_previousTriggerPairs.Contains(pair))
                {
                    // Enter
                    if (pair.Trigger is ITriggerEnterEvent triggerEnter)
                    {
                        // Debug.Log($"{pair.Actor} entered {pair.Trigger}");
                        triggerEnter.OnTriggerEntered(pair.Actor);
                    }
                }
                else
                {
                    // Stay
                    if (pair.Trigger is ITriggerStayEvent triggerStay)
                    {
                        // Debug.Log($"{pair.Actor} stayed in {pair.Trigger}");
                        triggerStay.OnTriggerStayed(pair.Actor);
                    }
                }
            }
            // Exit
            foreach (OverlapPair pair in _previousTriggerPairs)
            {
                // Debug.Log($"Checking pair containing {pair.Actor} for exit");
                // Debug.Log($"Is exit: {!_currentTriggerPairs.Contains(pair)}");
                if (!_currentTriggerPairs.Contains(pair))
                {
                    if (pair.Trigger is ITriggerExitEvent triggerExit)
                    {
                        // Debug.Log($"{pair.Actor} exited {pair.Trigger}");
                        triggerExit.OnTriggerExited(pair.Actor);
                    }
                }
            }
        }
    }
}