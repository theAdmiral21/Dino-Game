namespace Environment.Core.Interactions
{
    public interface IBarkZone : IInteractable
    {
        public void SubToBark(IInteractable interactable);
        public void UnsubToBark(IInteractable interactable);
    }
}