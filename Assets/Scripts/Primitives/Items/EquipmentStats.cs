using Primitives.Damage;

namespace Primitives.Items
{
    public struct EquipmentStats
    {
        public readonly int MagazineSize;
        public readonly float ReloadTime;
        public readonly float FireRate;
        public readonly int Damage;
        public readonly float KnockBack;
        public readonly float HitStun;
        public readonly DamageType HurtType;

        public EquipmentStats(
                                int magSize,
                                float reloadTime,
                                float fireRate,
                                int damage,
                                float knockBack,
                                float hitStun,
                                DamageType damageType)
        {
            MagazineSize = magSize;
            ReloadTime = reloadTime;
            FireRate = fireRate;
            Damage = damage;
            KnockBack = knockBack;
            HitStun = hitStun;
            HurtType = damageType;
        }
    }
}