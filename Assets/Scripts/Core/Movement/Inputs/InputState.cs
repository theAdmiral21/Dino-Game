using UnityEngine;
namespace Core.Movement.Inputs.DataStructures
{
    [System.Serializable]
    public struct InputState
    {
        public Vector2 LeftStick;
        public bool SprintPressed;
        public bool JumpPressed;
        public bool HoldingJump;
        public bool GrabPressed;
        public bool BarkPressed;
        public bool BarkHeld;
    }
}