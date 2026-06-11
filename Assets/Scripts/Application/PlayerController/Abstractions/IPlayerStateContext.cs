using PlayerController.Application.Physics.DataStructures;

namespace PlayerController.Application.Abstractions
{
    public interface IPlayerStateContext
    {
        public bool IsBouncing { get; set; }
        public bool IsSwinging { get; set; }
        public bool IsWallSliding { get; set; }
        public bool IsWallJumping { get; set; }
        public PlayerState CurrentState { get; }

        public void CheckState();

        public void SetState(PlayerState newState);
    }
}