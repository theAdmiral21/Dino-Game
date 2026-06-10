using UnityEngine;

namespace Movement.Core.Abstractions
{
    public interface IKnockBackable
    {
        public void KnockBack(float apexTime, Vector2 velocity);
    }
}