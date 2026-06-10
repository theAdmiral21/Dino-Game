using Movement.Core.Movement.DataStructures;

namespace Movement.Core.Abstractions
{
    public interface IExternalVelocityProvider
    {
        public IActionRequest GetVelocity();
    }
}