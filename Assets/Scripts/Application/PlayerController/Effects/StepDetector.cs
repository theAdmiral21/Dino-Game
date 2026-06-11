
using PlayerController.Core.Effects.DataStructures;
using Primitives.Audio;
using Primitives.Audio.Enums;
using Primitives.Physics;
using UnityEngine;

namespace PlayerController.Application.Effects
{
    public class StepDetector
    {
        private float _accumulatedDistance = 0f;
        private float _topRunSpeed;
        private float _topSprintSpeed;

        public StepDetector(float topRunSpeed, float topSprintSpeed)
        {
            _topRunSpeed = topRunSpeed;
            _topSprintSpeed = topSprintSpeed;
        }

        public StepEffect TryStep(PhysicsContext physicsContext, float stepDistance)
        {
            // Accumulate distance only when grounded.
            if (physicsContext.IsGrounded)
            {
                _accumulatedDistance += physicsContext.DeltaPosition.magnitude;
                if (_accumulatedDistance > stepDistance)
                {
                    _accumulatedDistance -= stepDistance;

                    float speed = NormalizeSpeed(physicsContext.Velocity.x);
                    return Approved(physicsContext.Surface, speed);
                }
                return Denied();
            }
            else
            {
                // Reset the distance traveled when airborne.
                _accumulatedDistance = 0;
                return Denied();
            }
        }

        private float NormalizeSpeed(float runSpeed)
        {
            if (runSpeed < _topSprintSpeed)
            {
                return runSpeed / _topRunSpeed;
            }
            return runSpeed / _topSprintSpeed;
        }

        private StepEffect Approved(SurfaceType surface, float speed01)
        {
            // Debug.Log($"Step effect approved");
            if (speed01 < .25)
            {
                return new StepEffect(true, PlayerSoundKey.Walk, surface, speed01);
            }
            else
            {
                return new StepEffect(true, PlayerSoundKey.Run, surface, speed01);
            }
        }

        private StepEffect Denied()
        {
            // Debug.Log($"Step effect denied");
            return new StepEffect(false, PlayerSoundKey.None, SurfaceType.None, 0);
        }
    }
}