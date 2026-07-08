namespace Core.Equipment
{
    public interface IFlashlight
    {
        public bool IsOn { get; }
        public float DischargeRate { get; }
        public float ChargeRate { get; }
        public void ToggleFlashlight();
    }
}