namespace PlayerController.Core.ManagerControls.Abstractions
{
    public interface IOverrideControls : IEnableDisablePlayer, ISetPlayerActive, IHaltCoroutines, IOverrideMove
    {
        public void ResetPlayer();
    }
}