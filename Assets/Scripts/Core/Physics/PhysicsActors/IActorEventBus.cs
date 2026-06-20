using System;
using Core.Equipment;
using Movement.Core.Movement.Abstractions;

namespace Physics.Core.PhysicsActors
{
    public interface IActorEventBus
    {
        public event Action<IActionResult> OnActionApproved;
        public event Action<IEquipmentActionResult> OnEquipmentActionApproved;
        public void Publish(IActionResult result);
        public void Publish(IEquipmentActionResult result);
    }
}