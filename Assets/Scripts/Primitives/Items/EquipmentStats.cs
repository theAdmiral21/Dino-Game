namespace Primitives.Items
{
    public struct EquipmentStats
    {
        public readonly int MagazineSize;
        public readonly float ReloadTime;
        public readonly float FireRate;
        public readonly float MuzzleVelocity;
        public readonly float SoundRadius;
        public readonly ProjectileStats Projectile;

        public EquipmentStats(
                                int magSize,
                                float reloadTime,
                                float fireRate,
                                float muzzleVelocity,
                                float soundRadius,
                                ProjectileStats projectileStats)
        {
            MagazineSize = magSize;
            ReloadTime = reloadTime;
            FireRate = fireRate;
            MuzzleVelocity = muzzleVelocity;
            SoundRadius = soundRadius;
            Projectile = projectileStats;
        }
    }
}