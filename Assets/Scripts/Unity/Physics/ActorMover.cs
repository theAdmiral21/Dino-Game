using System;
using Physics.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using Physics.Unity.Actors;
using Physics.Unity.Physics;
using Unity.Common.Unity;
using UnityEngine;

namespace Physics.Unity
{
    public class ActorMover : MonoBehaviour, IMoveActor
    {
        [SerializeField] private SerializedInterface<IRaycastController> _raycastController;


        public void MoveActor(IPhysicsActor actor, Vector2 velocity)
        {
            var actorTransform = actor.GetComponent<Transform>();
            actorTransform.Translate(velocity);
        }
        public void RotateActor(IPhysicsActor actor, float omega)
        {
            var actorTransform = actor.GetComponent<Transform>();
            actorTransform.Rotate(Vector3.up, omega);
        }
        public void SetPosition(IPhysicsActor actor, Vector2 position)
        {
            var actorTransform = actor.GetComponent<Transform>();
            // Check if the position is valid
            if (_raycastController.Interface.CheckFit(position, ref actor.Brain.FrameData.RaycastConfig))
            {
                actorTransform.position = position;
                // make sure everything stays in sync
                // _raycastController.Interface.UpdateRaycastOrigins();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }


    }
}