using Core.Physics.Collision.Callbacks;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Unity.Equipment.Throwables
{
    public class ThrowableContactHandler : MonoBehaviour, ICollisionEnterEvent
    {
        public void OnCollisionEntered(IPhysicsActor actor)
        {
            Debug.Log($"{name} collided with {actor.Name}");
        }
    }
}