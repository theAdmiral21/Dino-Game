using Primitives.Input;

namespace Infrastructure.Core.Inputs
{
    public interface IPlayerActionMapManager
    {
        public void EnableInputs();
        public void DisableInputs();
        public void SetActionMap(InputContext inputContext);
    }
}