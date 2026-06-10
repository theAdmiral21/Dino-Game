using Infrastructure.Core.Lifecycle.PhysicsEntities;
using Movement.Core.Abstractions;
using Movement.Core.Movement.DataStructures;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Primitives.Physics;
using UnityEngine;

namespace Physics.Core.PhysicsActors
{
    public interface IPhysicsActor : IDestructible, ISleepable
    {
        public ActorType Actor { get; }
        public IActorBrain Brain { get; }
        public IKinematicBody Body { get; }
        public string Name { get; }
        public Vector2 MoveVector { get; set; } // dafuq does this do? 

        public void EnqueueActionRequest(IActionRequest request);
        // You've found my dirty secret. Shield your eyes!
        public T GetComponent<T>();
        public T GetComponentInChildren<T>();
        public bool CompareTag(string tag);

        // public IBoundsProvider Bounds { get; }
        // public ITransformProvider TransformProvider { get; }

        // public RaycastConfiguration RayConfig { get; }
        // public BodyType ActorType { get; }

    }
}