using System;
using System.Collections;
using Application.Game.Audio.DataStructures;
using Application.Inventory;
using Core.Detection.Audio;
using Core.Equipment;
using Game.Core.Audio;
using Game.Core.Execution;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using Primitives.Damage;
using Primitives.Items;
using Unity.Common.Unity;
using Unity.Equipment.Abstractions;
using UnityEngine;

namespace Unity.Equipment
{
    public class SpasEquipment : BaseEquipment, IDamageDealer
    {
        [Header("Bullet Collision Layers")]
        [SerializeField] private LayerMask _layerMask;

        [Header("Emitters")]
        [SerializeField] private SerializedInterface<ISoundEmitter> _gunShotSoundMono;
        private ISoundEmitter _gunShotSound => _gunShotSoundMono.Interface;

        public override ItemType EquipmentType => ItemType.Shotgun;

        // public EquipmentStats Stats { get; private set; }


        // How many rounds are in your current magazine
        // public int RoundCount => _magazine.RoundCount;

        // public event Action<int> OnFire;
        // public event Action<int, Action<int>> OnReload;

        // private IMagazine _magazine;
        // private bool _weaponRaised;
        [SerializeField] private Transform _barrelEnd;
        private Vector2 _barrelPos => _barrelEnd.position;
        // private Vector2 _aimPos;
        private Vector2 _playerPos => new Vector2(transform.position.x, transform.position.y);

        private DamageInfo _damageInfo;

        [Header("Debug")]
        [SerializeField] private bool _debug;


        // [Header("Cheats")]
        // [SerializeField] private bool _hasInfiniteAmmo;
        // public bool HasInfiniteAmmo => _hasInfiniteAmmo;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;


        // private IGameContext _gameContext;
        // private ProjectileStats _projectileStats;
        private IAudioService _audioService;

        //         public void Init(EquipmentStats equipmentStats, IGameContext gameContext)
        //         {
        //             Stats = equipmentStats;
        //             _magazine = new Magazine(equipmentStats.MagazineSize);
        //             _projectileStats = Stats.Projectile;
        //             _gameContext = gameContext;
        //             _audioService = gameContext.AudioService;
        //             gameObject.GetComponentInChildren<IInitStats<EquipmentStats>>().Init(Stats);

        // #if !UNITY_EDITOR
        //             _hasInfiniteAmmo = false;
        // #endif
        //         }

        public override void PostInit(EquipmentStats equipmentStats, IGameContext gameContext)
        {
            _audioService = gameContext.AudioService;
            gameObject.GetComponentInChildren<IInitStats<EquipmentStats>>().Init(Stats);

#if !UNITY_EDITOR
                    _hasInfiniteAmmo = false;
#endif
        }


        public override void Aim(Vector2 mosPos)
        {
            // Draw a cross hair
            _aimPos = Camera.main.ScreenToWorldPoint(mosPos);
        }

        public override void Fire()
        {
            if (_magazine.ConsumeRound())
            {
                StartCoroutine(FireRoutine());
            }
            else
            {
                RequestReload();
            }
        }
        private IEnumerator FireRoutine()
        {
            // OnFire?.Invoke(_magazine.RoundCount);
            RaiseOnFire(_magazine.RoundCount);

            _audioService.PlaySFX(
                new SoundRequest(
                    EntityKey.Shotgun,
                    ActionSoundKey.Attack));
            DamageCast();
            _gunShotSound.EmitSound();
            yield return new WaitForSeconds(Stats.FireRate);
            _audioService.PlaySFX(
                new SoundRequest(
                    EntityKey.Shotgun,
                    ActionSoundKey.PickUp));
            yield return new WaitForSeconds(0.5f);
            // After firing, wait then reload
        }
        private void DamageCast()
        {
            // Perform a ray cast in the direction the player is aiming
            Vector2 _aimDir = (_aimPos - _barrelPos).normalized;

            // fire 5 raycasts in a cone with varying angles
            float spreadAngle = 15;

            for (int i = 0; i < _projectileStats.QuantityPerShot; i++)
            {
                float angle = UnityEngine.Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                Vector2 fuzzyDir = RotateVector(_aimDir, angle);
                // perform the cast with infinite range?
                RaycastHit2D hit = Physics2D.Raycast(_barrelPos, fuzzyDir, 100f, _layerMask);

                if (_debug)
                {
                    Debug.DrawRay(_barrelPos, fuzzyDir * 100, Color.red, 1f);
                }

                if (hit.collider != null)
                {
                    IDamageable damageable = hit.collider.GetComponentInChildren<IDamageable>();
                    InflictDamage(damageable);
                }
            }
        }
        public void InflictDamage(IDamageable damageable)
        {
            if (damageable == null) return;

            damageable.ReceiveDamage(new DamageInfo(DamageType.Hurt,
                                _projectileStats.KnockBack * (_aimPos - _barrelPos).normalized,
                                _projectileStats.KnockBack,
                                _projectileStats.HitStun,
                                _projectileStats.Damage));
        }
        private Vector2 RotateVector(Vector2 v, float degrees)
        {
            float rad = degrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            return new Vector2(
                v.x * cos - v.y * sin,
                v.x * sin + v.y * cos
            );
        }
        // public void RaiseWeapon(bool raiseWeapon)
        // {
        //     _weaponRaised = raiseWeapon;
        //     if (_weaponRaised)
        //     {

        //     }
        //     else
        //     {

        //     }
        // }
        public override void RequestReload()
        {
            int requestAmount = _magazine.Capacity - _magazine.RoundCount;
            // Debug.Log($"Requesting: {requestAmount} rocks");
            if (_hasInfiniteAmmo)
            {
                _magazine.ReplenishRounds(_magazine.Capacity);
            }
            else
            {
                // OnReload?.Invoke(requestAmount, _magazine.ReplenishRounds);
                RaiseOnReload(requestAmount, _magazine.ReplenishRounds);
            }
        }

        // private void DrawCrossHair()
        // {
        //     Debug.DrawLine(_barrelEnd.position, _aimPos);
        // }

        // private void Update()
        // {
        //     if (_weaponRaised)
        //     {
        //         DrawCrossHair();
        //     }
        // }
    }
}