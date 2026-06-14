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
        public IInventorySystem InventorySystem { get; }
        public int RoundCount { get; } // This will mean different things for different objects. For example for guns it means rounds left in the magazine. For throwables it is how much you have in your inventory.

        // Equipment behavior
        public event Action OnInventoryEmpty;

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