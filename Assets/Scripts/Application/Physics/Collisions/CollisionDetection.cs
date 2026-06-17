using System.Collections.Generic;
using UnityEngine;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Primitives.Physics;
using Physics.Core.PhysicsActors;

namespace Physics.Application.Collisions
{
    public class CollisionDetection : IDetectCollision
    {
        public HashSet<CollidingPair> Collisions => _collisions;
        private HashSet<CollidingPair> _collisions = new();
        private Dictionary<IPhysicsActor, Vector2> _resolveDict = new();
        public Dictionary<IPhysicsActor, Vector2> GetCollisions(List<IPhysicsActor> actors)
        {
            _collisions.Clear();
            // Compare all of the actors to one another
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i].IsAsleep) continue;
                for (int j = i + 1; j < actors.Count; j++)
                {
                    if (IsColliding(actors[i], actors[j]))
                    {
                        _collisions.Add(new CollidingPair(actors[i], actors[j]));
                        // For now let's see if this works.
                        Debug.Log($"[CollisionDetection] Got collision between {actors[i].Name} and {actors[j].Name}");
                    }
                }
            }
            return ResolveCollisions();
        }

        private bool IsColliding(IPhysicsActor actorA, IPhysicsActor actorB)
        {
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

        public Dictionary<IPhysicsActor, Vector2> ResolveCollisions()
        {
            _resolveDict.Clear();
            foreach (var collision in _collisions)
            {
                Vector2 sepVector = collision.SeparationVector;
                // Only move the actor
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

        private void CallCollision
    }
}