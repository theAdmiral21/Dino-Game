using System.Collections.Generic;
using Core.Physics.Abstractions;
using Core.Physics.Triggers;
using Core.Physics.Triggers.Callbacks;
using Gameplay.Common.Application.DataStructures;
using Infrastructure.Unity.Registries;
using Physics.Core.PhysicsActors;
using Primitives.Physics;

namespace Physics.Collisions
{
    public class TriggerDetection : IDetectTrigger
    {
        public IReadOnlyCollection<IPhysicsActor> ActorRegistry => _physicsRegistry.Actors;
        public IReadOnlyCollection<ITriggerVolume> TriggerRegistry => _physicsRegistry.Triggers;
        private HashSet<OverlapPair> _previousTriggerPairs = new();
        private HashSet<OverlapPair> _currentTriggerPairs = new();
        private IPhysicsRegistry _physicsRegistry;
        public TriggerDetection(IPhysicsRegistry physicsRegistry)
        {
            _physicsRegistry = physicsRegistry;
        }

        public void ResolveTriggers()
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