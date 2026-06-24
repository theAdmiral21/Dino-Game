using Core.Movement.Inputs;
using UnityEngine;

namespace Enemy.Unity.Providers
{
    public class EnemyInputProvider : MonoBehaviour, IActorInput
    {
        public Vector2 Move => Vector2.right;

        public bool JumpPressed => false;

        public bool JumpHeld => false;
    }
}