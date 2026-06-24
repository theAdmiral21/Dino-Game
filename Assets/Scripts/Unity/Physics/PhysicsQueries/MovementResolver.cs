using System.Collections.Generic;
using Core.Physics.Collisions.DataStructures;
using Physics.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using UnityEngine;

namespace Physics.Unity.Movement
{
    public class MovementResolver : IMovementResolver
    {
        private readonly IRaycastController _rayCaster;
        private Vector2 _velocity;
        public MovementResolver(IRaycastController raycastController)
        {
            _rayCaster = raycastController;
        }

        public MovementResolution ResolveMovement(Vector2 velocity, RaycastConfiguration rayConfig)
        {
            rayConfig.UpdateRaycastOrigins();
            // Clear all collisions from the previous frame
            _rayCaster.ResetCollisions();

            // Debug.Log($"Resolving velocity: {velocity}");
            Vector2 nudge = Vector2.zero;
            if (velocity.x != 0)
            {
                nudge += _rayCaster.HorizontalRaycast(ref velocity, ref rayConfig);
            }

            if (velocity.y != 0)
            {
                nudge += _rayCaster.VerticalRaycast(ref velocity, ref rayConfig);
            }

            if (velocity.x != 0 && velocity.y != 0)
            {
                _rayCaster.CornerRayCast(ref velocity, ref rayConfig);
            }

            // Get the collisions for this frame
            List<RayCollision> collidingActors = _rayCaster.GetCollisions();


            //NOTE Why do I have this line? To persist state?
            _velocity = velocity;

            // Debug.Log($"Total Nudge: {nudge}");
            return new MovementResolution
            {
                FrameDelta = _velocity,
                CornerNudge = nudge,
                RayCollisions = collidingActors
            };
        }
    }
}