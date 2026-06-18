using Primitives.Damage;
using Primitives.Items;
using UnityEngine;
using UnityEngine.Windows;

namespace Unity.Equipment.DataStructures
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Game/Items/Equipment Stats")]
    public class EquipmentSO : ScriptableObject
    {
        public int MagazineSize;
        public float ReloadTime;
        public float FireRate;
        public int Damage;
        public float KnockBack;
        public float HitStun;
        public DamageType HurtType;
        public float MuzzleVelocity;

        public EquipmentStats BuildRunTime()
        {
            return new EquipmentStats(
                                MagazineSize,
                                ReloadTime,
                                FireRate,
                                Damage,
                                KnockBack,
                                HitStun,
                                HurtType,
                                MuzzleVelocity
                            );
        }

    }
}