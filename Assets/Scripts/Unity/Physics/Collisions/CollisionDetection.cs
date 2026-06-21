using System.Collections.Generic;
using UnityEngine;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Primitives.Physics;
using Physics.Core.PhysicsActors;
using Core.Physics.Collisions.DataStructures;
using Core.Physics.Triggers.Callbacks;
using Core.Physics.Collision.Callbacks;
using Movement.Core.Rules;

namespace Physics.Unity.Collisions
{
    public class CollisionDetection : IDetectCollision
    {
        public HashSet<CollidingPair> CurrentCollisions => _currentCollisions;
        private HashSet<CollidingPair> _currentCollisions = new();
        private HashSet<CollidingPair> _previousCollisions = new();
        private Dictionary<IPhysicsActor, Vector2> _resolveDict = new();

        public Dictionary<IPhysicsActor, Vector2> GetCollisions(List<IPhysicsActor> actors)
        {
            _currentCollisions.Clear();
            // Get all of the raycast collisions
            for (int i = 0; i < actors.Count; i++)
            {
                var actor = actors[i];
                for (int j = 0; j < actor.Brain.FrameData.RayCollisions.Count; j++)
                {
                    List<RayCollision> collision = actor.Brain.FrameData.RayCollisions;
                    var otherActor = GetActorHelper(collision[j]);
                    if (otherActor != null)
                    {
                        Debug.Log($"Adding an actor");
                        _currentCollisions.Add(new CollidingPair(actor, otherActor));
                    }
                    else
                    {
                        Debug.Log($"Adding a ray collision");
                        _currentCollisions.Add(new CollidingPair(actor, collision[j]));
                    }
                }
            }

            // Now run AABB on everything without a raycast collision
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i].IsAsleep) continue;
                for (int j = i + 1; j < actors.Count; j++)
                {
                    if (actors[j].IsAsleep) continue;

                    var candidatePair = new CollidingPair(actors[i], actors[j]);

                    if (_currentCollisions.Contains(candidatePair)) continue;

                    // we have a fresh pair, check for a collision
                    if (IsColliding(actors[i], actors[j]))
                    {
                        _currentCollisions.Add(candidatePair);
                    }
                }
            }
            return ResolveCollisions();
        }


        private IPhysicsActor GetActorHelper(RayCollision rayCollision)
        {
            rayCollision.HitInfo.collider.TryGetComponent<IPhysicsActor>(out var actor);
            return actor;
        }


        // So my raycast is detecting collisions and so is my AABB collision detector. How do I tell them to work together? 
        // if actor.Brain.FrameData.CollidingActors.Count > 0 -> Colliding = yes, process separately?
        private bool IsColliding(IPhysicsActor actorA, IPhysicsActor actorB)
        {
            if (!CanCollide(actorA, actorB)) return false;
            // Get the bounds for each actor
            AABB A = actorA.Body.Bounds.GetBounds();
            AABB B = actorB.Body.Bounds.GetBounds();

            // Perform the check
            // Debug.Log($"A.Name: {actorA}; A.Center: {A.Center}, A.Extents: {A.Extents}; B.Name: {actorB};  B.Center: {B.Center}, B.Extents: {B.Extents}");

            // A is right of B
            bool aRightB = A.Left > B.Right;
            bool aLeftB = A.Right < B.Left;
            bool aAboveB = A.Bottom > B.Top;
            bool aBelowB = A.Top < B.Bottom;

            return !(
                aRightB ||
                aLeftB ||
                aAboveB ||
                aBelowB
                );
        }

        private bool CanCollide(IPhysicsActor actorA, IPhysicsActor actorB)
        {
            // if (actorA == null || actorB == null) return false;
            // Debug.Log($"Actors are not null");
            // if (actorA.Body == null || actorB.Body == null) return false;
            // Debug.Log($"Actors bodies are not null");
            // if (actorA.Body.RayConfig == null || actorB.Body.RayConfig == null) return false;
            // Debug.Log($"Actors raycast configurations are not null");
            // Compare mask A against layer B
            int maskA = actorA.Body.RayConfig.CollisionLayer;
            int layerB = actorB.Body.RayConfig.PhysicalLayer;

            return (maskA & (1 << layerB)) != 0;
        }

        public Dictionary<IPhysicsActor, Vector2> ResolveCollisions()
        {
            _resolveDict.Clear();
            // if (_currentCollisions.Count == 0) Debug.Log($"No collisions to resolve");
            foreach (var collision in _currentCollisions)
            {
                // Debug.Log($"Resolving collision between {collision.ActorA} and {collision.ActorB}");
                // Only move the actor
                if (collision.ActorB != null)
                {
                    if (collision.ActorA.Body.BodyType == BodyType.Static &&
                        collision.ActorB.Body.BodyType == BodyType.Kinematic)
                    {
                        UpdateDictionary(collision.ActorB, collision.SeparationVector);
                    }
                    else if (collision.ActorA.Body.BodyType == BodyType.Kinematic &&
                            collision.ActorB.Body.BodyType == BodyType.Static)
                    {
                        UpdateDictionary(collision.ActorA, -collision.SeparationVector);

                    }
                    else if (collision.ActorA.Body.BodyType == BodyType.Kinematic &&
                            collision.ActorB.Body.BodyType == BodyType.Kinematic)
                    {
                        UpdateDictionary(collision.ActorA, -collision.SeparationVector / 2);
                        UpdateDictionary(collision.ActorB, collision.SeparationVector / 2);
                    }
                    else
                    {
                        Debug.LogError("Statics shouldn't collide... right?");
                    }
                }
                // else
                // {
                //     // When actorB is null, we've collided with a static object.
                //     UpdateDictionary(collision.ActorA, -collision.SeparationVector);
                // }
            }

            // Dispatch collision events
            DiffAndDispatch();
            // track previous collisions
            _previousCollisions = new HashSet<CollidingPair>(_currentCollisions);
            return _resolveDict;
        }

        private void UpdateDictionary(IPhysicsActor actor, Vector2 separationVector)
        {
            if (_resolveDict.ContainsKey(actor))
            {
                _resolveDict[actor] += separationVector;
            }
            else
            {
                _resolveDict[actor] = separationVector;
            }
        }

        private void DiffAndDispatch()
        {
            // Check enter and stay
            foreach (CollidingPair pair in _currentCollisions)
            {
                if (!_previousCollisions.Contains(pair))
                {
                    Debug.Log($"Collision entered for {pair.ActorA} and {pair.ActorB}");
                    if (pair.ActorA.CollisionHandler is ICollisionEnterEvent collisionEnterA)
                    {
                        Debug.Log($"Calling collision entered for {pair.ActorA}");
                        collisionEnterA.OnCollisionEntered(pair.CollisionInfoB());

                    }
                    // if (pair.ActorB != null && pair.ActorB.CollisionHandler is ICollisionEnterEvent collisionEnterB)
                    // {
                    //     collisionEnterB.OnCollisionEntered(pair.CollisionInfoA());
                    // }
                }
                else
                {
                    if (pair.ActorA.CollisionHandler is ICollisionStayedEvent collisionEnterA)
                    {
                        collisionEnterA.OnCollisionStayed(pair.ActorB);
                    }
                    if (pair.ActorB != null && pair.ActorB.CollisionHandler is ICollisionStayedEvent collisionEnterB)
                    {
                        collisionEnterB.OnCollisionStayed(pair.ActorA);
                    }
                }
            }
            // collision exit
            foreach (CollidingPair pair in _previousCollisions)
            {
                if (!_currentCollisions.Contains(pair))
                {
                    if (pair.ActorA.CollisionHandler is ICollisionExitEvent collisionEnterA)
                    {
                        collisionEnterA.OnCollisionExit(pair.ActorB);
                    }
                    if (pair.ActorB != null && pair.ActorB.CollisionHandler is ICollisionExitEvent collisionEnterB)
                    {
                        collisionEnterB.OnCollisionExit(pair.ActorA);
                    }
                }
            }
        }
    }
}