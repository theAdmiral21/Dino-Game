namespace Environment.Core.Interactions
{
    public interface IInteractable
    {
        public void Interact();
        public bool CanInteract();
    }
}