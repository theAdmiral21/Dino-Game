
namespace Core.Movement.Inputs
{
    public interface IPlayerInputs : IActorInput
    {
        public bool SprintPressed { get; }
        public bool RaiseWeapon { get; }
        public bool DodgePressed { get; }
        public bool DodgeHeld { get; }
        public bool CrouchPressed { get; }
    }
}