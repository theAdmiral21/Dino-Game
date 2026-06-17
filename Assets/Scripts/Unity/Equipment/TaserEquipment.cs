using System;
using Core.Equipment;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment
{
    public class TaserEquipment : MonoBehaviour, IEquipment
    {
        public ItemType EquipmentType => ItemType.Taser;

        public EquipmentStats Stats { get; private set; }

        public int RoundCount => 1;

        public event Action OnFire;
        public event Action OnReload;

        public TaserEquipment(EquipmentStats stats)
        {
            Stats = stats;
        }

        public void Aim()
        {
            Debug.Log($"Aiming Taser");
        }

        public void Fire()
        {
            Debug.Log($"Firing Taser");
        }

        public void RaiseWeapon()
        {
            Debug.Log($"Raising Taser");
        }

        public void Reload()
        {
            Debug.Log($"Reloading Taser");
        }
    }
}