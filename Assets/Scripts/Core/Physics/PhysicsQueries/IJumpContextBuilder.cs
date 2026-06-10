using Movement.Core.DataStructures;
using Physics.Core.DataStructures;
using UnityEngine;

namespace Physics.Core.PhysicsQueries
{
    public interface IJumpContextBuilder
    {
        public JumpContext BuildJumpContext(RaycastConfiguration raycastConfig, Vector2 velocity, float gravity, float facing);
    }
}