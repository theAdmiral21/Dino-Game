using Core.Physics.Collisions;
using Infrastructure.Core.Lifecycle.PhysicsEntities;
using Movement.Core.Movement.DataStructures;
using Physics.Core.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace Physics.Core.PhysicsActors
{
    public interface IPhysicsActor : IDestructible, ISleepable
    {
        public ActorType Actor { get; }
        public IActorBrain Brain { get; }
        public IKinematicBody Body { get; }
        public ICollisionHandler CollisionHandler { get; }
        public string Name { get; }
        public Vector2 MoveVector { get; set; } // dafuq does this do? 

        public void EnqueueActionRequest(IActionRequest request);
        // You've found my dirty secret. Shield your eyes!
        public T GetComponent<T>();
        public T GetComponentInChildren<T>();
        public bool CompareTag(string tag);
    }
}