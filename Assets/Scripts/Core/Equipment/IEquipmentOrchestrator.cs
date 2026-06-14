namespace Core.Equipment
{
    public interface IEquipmentOrchestrator
    {
        public IEquipmentBridge EquipmentBridge { get; }

        public void EnqueueEquipmentRequest(IEquipmentActionRequest request);

        public void ResolveRequests();

        public void Tick(float dt);

    }
}