using Movement.Core.Movement.DataStructures;

namespace Movement.Core.Abstractions
{
    /// <summary>
    /// Any entity that can receive forces from an external source.
    /// </summary>
    public interface IExternalForceReceiver
    {
        // public PhysicsContext PhysicsContext { get; }
        /// <summary>
        /// Method for applying an impulse, ie: explosion, bounce pad, stomp
        /// </summary>
        /// <param name="velocity"></param>
        public void ReceiveImpulse(IActionRequest result);
        /// <summary>
        /// Method for applying a continuous force, ie: conveyor belt, wind, moving platform
        /// </summary>
        /// <param name="velocity"></param>
        public void ReceiveContinuous(IActionRequest result);
    }
}