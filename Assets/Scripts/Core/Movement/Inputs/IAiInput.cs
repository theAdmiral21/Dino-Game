using UnityEngine;

namespace Core.Movement.Inputs
{
    /// <summary>
    /// Abstraction layer for IActorInput that allows NPCs to send input commands to move and what not.
    /// </summary>
    public interface IAiInput : IActorInput
    {
        public void SetBackUp(Vector2 input);
        public void SetMove(Vector2 input);
        public void SetJumpPressed(bool input);
        public void SetJumpHeld(bool input);
        public void FaceLeft(bool input);
    }
}