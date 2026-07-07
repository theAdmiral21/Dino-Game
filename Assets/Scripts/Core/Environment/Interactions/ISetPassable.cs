using Physics.Core.Abstractions;
using Physics.Core.DataStructures;

namespace Core.Environment.Interactions
{
    public interface ISetPassable
    {
        public void SetPassable(RaycastConfiguration rayConfig, bool passable);
    }
}