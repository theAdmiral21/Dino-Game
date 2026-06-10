using System;
using Movement.Core.Movement.Abstractions;

namespace Physics.Core.PhysicsActors
{
    public interface IActorEventBus
    {
        public event Action<IActionResult> OnActionApproved;
        public void Publish(IActionResult result);
    }
}