
namespace Movement.Core.Inputs
{
    public interface IPlayerInputs : IActorInput
    {
        public bool SprintPressed { get; }
        public bool GrabPressed { get; }
        public bool DodgePressed { get; }
        public bool DodgeHeld { get; }
        public bool CrouchPressed { get; }
    }
}