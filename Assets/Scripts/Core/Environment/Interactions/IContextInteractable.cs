
using Game.Core.Interactions;

namespace Environment.Core.Interactions
{
    public interface IContextInteractable : IInteractable
    {
        public void Interact(IInteractContext context);
    }
}