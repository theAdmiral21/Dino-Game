using UnityEngine;

namespace Core.Movement.Inputs
{
    public interface IRaptorInput : IActorInput, IAiInput
    {
        public bool LungePressed { get; }
        public void Lunge(Vector2 direction);
    }
}