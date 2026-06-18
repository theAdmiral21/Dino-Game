using Movement.Core.Inputs;
using UnityEngine;

namespace NPC.Unity.Providers
{
    public class NullInputProvider : MonoBehaviour, IActorInput
    {
        public Vector2 Move => Vector2.zero;

        public bool JumpPressed => false;

        public bool JumpHeld => false;
    }
}