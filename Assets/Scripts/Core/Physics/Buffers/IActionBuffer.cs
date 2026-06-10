using UnityEngine;
using Movement.Core.Movement.DataStructures;
using Physics.Core.DataStructures;

namespace Physics.Core.Buffers
{
    public interface IActionBuffer
    {
        public JumpRequest BufferJumpRequest(RaycastConfiguration raycastConfig, float bufferDuration, Vector2 velocity, float gravity);
    }
}