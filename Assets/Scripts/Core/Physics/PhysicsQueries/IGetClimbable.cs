using Core.Environment.Interactions;
using Physics.Core.DataStructures;

namespace Core.Physics.PhysicsQueries
{
    public interface IGetClimbable
    {
        public IClimbable FindClimbable(RaycastConfiguration rayConfig);
    }
}