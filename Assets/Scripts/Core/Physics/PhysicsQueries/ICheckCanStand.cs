using Physics.Core.DataStructures;

namespace Core.Physics.PhysicsQueries
{
    public interface ICheckCanStand
    {
        public bool CheckCanStand(RaycastConfiguration rayConfig);
    }
}