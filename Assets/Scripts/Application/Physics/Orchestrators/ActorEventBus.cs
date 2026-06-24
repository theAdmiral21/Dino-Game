using System;
using Core.Equipment;
using Movement.Core.Movement.Abstractions;
using Physics.Core.PhysicsActors;

namespace Physics.Application.Orchestrators
{
    public class ActorEventBus : IActorEventBus
    {
        public event Action<IActionResult> OnActionApproved;
        public event Action<IEquipmentActionResult> OnEquipmentActionApproved;
        public void Publish(IActionResult result)
        {
            OnActionApproved?.Invoke(result);
        }
        public void Publish(IEquipmentActionResult result)
        {
            OnEquipmentActionApproved?.Invoke(result);
        }
    }
}