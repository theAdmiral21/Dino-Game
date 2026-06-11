using UnityEngine;

namespace PlayerController.Core.ManagerControls.Abstractions
{
    public interface IOverrideMove
    {
        public void OverrideMove(Vector2 newPosition);
    }
}