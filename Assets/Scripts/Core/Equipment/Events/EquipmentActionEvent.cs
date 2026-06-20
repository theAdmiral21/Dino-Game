namespace Core.Equipment.Events
{
    public record EquipmentActionEvent
    {
        public readonly IEquipmentActionResult Result;

        public EquipmentActionEvent(IEquipmentActionResult result) => Result = result;
    }
}