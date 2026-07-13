using System;
using Game.Core.Execution;
using Primitives.Items;
using UnityEngine;

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

        // Magazine getter
        public IMagazine Magazine { get; }

        // Effect notification
        public event Action<int> OnFire;
        public event Action<int, Action<int>> OnReload;

        // Equipment orchestrators
        public void RaiseWeapon(bool raiseWeapon);
        public void Aim(Vector2 mosPos); // this will probably need some request argument
        public void RequestReload();
        public void Fire();
        public void Init(EquipmentStats stats, IGameContext gameContext);

        // Unity methods that are really helpful
        public T GetComponent<T>();
        public T GetComponentInChildren<T>();
    }
}