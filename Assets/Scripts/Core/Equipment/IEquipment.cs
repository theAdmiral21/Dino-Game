using System;
using Core.Inventory;
using Primitives.Items;

namespace Core.Equipment
{
    public interface IEquipment
    {
        // Equipment identification
        public ItemType EquipmentType { get; }

        // Equipment stats
        public EquipmentStats Stats { get; }

        // Equipment state information
        public int RoundCount { get; }

        // Equipment behavior


        // Effect notification
        public event Action OnFire;
        public event Action OnReload;

        // Equipment orchestrators
        public void RaiseWeapon();
        public void Aim(); // this will probably need some request argument
        public void Reload();
        public void Fire();
    }
}