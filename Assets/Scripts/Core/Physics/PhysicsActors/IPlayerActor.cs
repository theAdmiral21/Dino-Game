using Movement.Core.Movement.DataStructures;

namespace Physics.Core.PhysicsActors
{
    public interface IPlayerActor
    {
        public void BufferRequests(IActionRequest newRequest);
    }
}