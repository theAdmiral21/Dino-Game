using System;
using Movement.Core.Movement.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;

namespace Physics.Application.Orchestrators
{
    public class ActorEventBus : IActorEventBus
    {
        public event Action<IActionResult> OnActionApproved;
        public void Publish(IActionResult result)
        {
            OnActionApproved?.Invoke(result);
        }
    }
}