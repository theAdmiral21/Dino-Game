using Primitives.EventBus.Abstractions;

namespace Core.Equipment
{
    public interface IEquipmentBridge
    {
        public IEquipment Equipped { get; }

        public void RouteEquipmentResult(IEquipmentActionResult result);

        public void SetEventBus(IEventBus eventBus);
    }
}