using Physics.Core.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace Physics.Features
{
    /// <summary>
    /// Public class used to integrate a requested velocity and acceleration to solve for an objects position in the next frame.
    /// </summary>
    public class Integrator : IIntegrator
    {
        /// <summary>
        /// Method taking an actor's KinematicResult (kinematic state) and integrates velocity and acceleration to find the displacement for this frame.
        /// </summary>
        /// <param name="kinematicState"></param>
        /// <returns></returns>
        public KinematicResult Integrate(ref KinematicResult kinematicState)
        {
            kinematicState = TranslationalIntegral(ref kinematicState);

            kinematicState = RotationalIntegral(ref kinematicState);

            return kinematicState;
        }

        private KinematicResult TranslationalIntegral(ref KinematicResult kinematicState)
        {
            // Apply dt
            kinematicState.Velocity += (kinematicState.Acceleration + Vector2.up * kinematicState.Gravity) * kinematicState.Dt;

            // Add external velocities
            Vector2 finalVelocity = kinematicState.Velocity + kinematicState.ExternalVelocity;

            kinematicState.FrameDelta = finalVelocity * kinematicState.Dt;

            return kinematicState;
        }

        private KinematicResult RotationalIntegral(ref KinematicResult kinematicState)
        {
            // Apply dt
            kinematicState.RotationalVelocity += kinematicState.RotationalAcceleration * kinematicState.Dt;

            kinematicState.AngularFrameDelta = kinematicState.RotationalVelocity * kinematicState.Dt;

            return kinematicState;
        }
    }
}