using Physics.Core.DataStructures;

namespace Core.Environment.Interactions
{
    public interface ISetPassable : IPassable
    {
        public void SetPassable(RaycastConfiguration rayConfig, bool passable);
    }
}