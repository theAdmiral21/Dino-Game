using UnityEngine;

namespace Core.Movement.Inputs
{
    public interface IActorInput
    {
        public Vector2 Move { get; }
        public bool JumpPressed { get; }
        public bool JumpHeld { get; }
    }
}