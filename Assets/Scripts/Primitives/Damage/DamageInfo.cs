using UnityEngine;

namespace Primitives.Damage
{
    public struct DamageInfo
    {
        public DamageType DamageType;
        public Vector2 KnockBackVelocity;
        public float KnockBackApex;
        public float StunTime;
        public int DamageValue;

        public DamageInfo(DamageType damageType, Vector2 knockBackVelocity, float knockBackApex, float stunTime, int damageValue)
        {
            DamageType = damageType;
            KnockBackVelocity = knockBackVelocity;
            KnockBackApex = knockBackApex;
            StunTime = stunTime;
            DamageValue = damageValue;
        }
    }
}