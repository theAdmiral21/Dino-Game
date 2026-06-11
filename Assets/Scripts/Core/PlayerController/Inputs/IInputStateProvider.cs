using Movement.Core.Inputs.DataStructures;

namespace PlayerController.Core.Inputs
{
    public interface IInputStateProvider
    {
        public InputState Inputs { get; }
    }
}