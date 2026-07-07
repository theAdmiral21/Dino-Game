using Core.Environment.Interactions;
using Physics.Core.DataStructures;
using Primitives.Physics.Enums;

namespace Core.Physics.PhysicsQueries
{
    public interface IGetClimbable
    {
        public IClimbable FindClimbable(RaycastConfiguration rayConfig);
    }
}