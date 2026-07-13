using System;
using Application.Inventory;
using Core.Equipment;
using Core.Game.Lifecycle;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Primitives.Items;
using Primitives.SaveData;
using UnityEngine;

namespace Unity.Equipment.Abstractions
{
    public abstract class BaseEquipment : MonoBehaviour, IAllowInfiniteAmmo, IEquipment
    {
        public IMagazine Magazine => _magazine;
        protected IMagazine _magazine;
        protected Vector2 _aimPos;
        private bool _weaponRaised;

        protected IGameContext _gameContext;
        protected ProjectileStats _projectileStats;
        public EquipmentStats Stats { get; private set; }
        public int RoundCount => _magazine.RoundCount;

        [Header("Cheats")]
        [SerializeField] protected bool _hasInfiniteAmmo;
        public bool HasInfiniteAmmo => _hasInfiniteAmmo;

        public event Action<int> OnFire;
        public event Action<int, Action<int>> OnReload;


        public abstract ItemType EquipmentType { get; }

        public void Init(EquipmentStats equipmentStats, IGameContext gameContext)
        {
            Stats = equipmentStats;
            _magazine = new Magazine(equipmentStats.MagazineSize);
            _projectileStats = Stats.Projectile;
            _gameContext = gameContext;
            PostInit(Stats, _gameContext);
        }

        public abstract void PostInit(EquipmentStats equipmentStats, IGameContext gameContext);

        public void RaiseWeapon(bool raiseWeapon)
        {
            _weaponRaised = raiseWeapon;
        }
        protected void RaiseOnFire(int roundCount) => OnFire?.Invoke(roundCount);
        protected void RaiseOnReload(int requestAmount, Action<int> replenish) => OnReload?.Invoke(requestAmount, replenish);



        private void DrawCrossHair()
        {
            Debug.DrawLine(transform.position, _aimPos);
        }

        private void Update()
        {
            if (_weaponRaised)
            {
                DrawCrossHair();
            }
        }

        public abstract void Aim(Vector2 mosPos);
        public abstract void RequestReload();
        public abstract void Fire();
    }
}