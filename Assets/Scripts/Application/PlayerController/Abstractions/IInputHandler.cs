namespace PlayerController.Application.Abstractions
{
    public interface IInputHandler
    {
        public void FreezeMovement();
        public void ThawMovement();
        public void Jump();
        public void StopJump();
        public void SetSprintStatus();
        public void Move();
    }
}