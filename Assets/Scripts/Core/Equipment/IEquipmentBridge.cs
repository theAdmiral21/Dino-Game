using Movement.Core.Movement.Abstractions;
using Physics.Core.PhysicsActors;

namespace Core.Equipment
{
    public interface IEquipmentBridge
    {
        public IEquipment Equipped { get; }

        public void RouteEquipmentResult(IActionResult result);
    }
}