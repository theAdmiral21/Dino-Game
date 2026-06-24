using Primitives.Damage;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment.DataStructures
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Game/Items/Equipment Stats")]
    public class EquipmentSO : ScriptableObject
    {
        public int MagazineSize;
        public float ReloadTime;
        public float FireRate;
        public DamageType HurtType;
        public float MuzzleVelocity;
        public float SoundRadius;
        public ProjectileSO ProjectileStats;

        public EquipmentStats BuildRunTime()
        {
            return new EquipmentStats(
                                MagazineSize,
                                ReloadTime,
                                FireRate,
                                MuzzleVelocity,
                                SoundRadius,
                                ProjectileStats.BuildRunTime()
                            );
        }

    }
}