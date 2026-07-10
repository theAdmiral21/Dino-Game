using Primitives.Damage;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment.DataStructures
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Game/Items/Projectile Stats")]
    public class ProjectileSO : ScriptableObject
    {
        public int Damage;
        public float KnockBack;
        public float HitStun;
        public DamageType HurtType;
        public float SoundRadius;
        public int QuantityPerShot;

        public ProjectileStats BuildRunTime()
        {
            return new ProjectileStats(
                                Damage,
                                KnockBack,
                                HitStun,
                                HurtType,
                                SoundRadius,
                                QuantityPerShot
                            );
        }

    }
}