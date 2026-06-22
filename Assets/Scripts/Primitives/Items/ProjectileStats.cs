using Primitives.Damage;

namespace Primitives.Items
{
    public struct ProjectileStats
    {
        public readonly int Damage;
        public readonly float KnockBack;
        public readonly float HitStun;
        public readonly DamageType HurtType;
        public readonly float SoundRadius;

        public ProjectileStats(

                                int damage,
                                float knockBack,
                                float hitStun,
                                DamageType damageType,
                                float soundRadius
                                )
        {

            Damage = damage;
            KnockBack = knockBack;
            HitStun = hitStun;
            HurtType = damageType;
            SoundRadius = soundRadius;
        }
    }
}
