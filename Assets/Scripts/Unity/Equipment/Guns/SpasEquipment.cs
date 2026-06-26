using System;
using System.Collections;
using Application.Inventory;
using Core.Detection.Audio;
using Core.Equipment;
using Game.Application.Audio.DataStructures;
using Game.Core.Audio;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Movement.DataStructures;
using Physics.Core.PhysicsActors;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using Primitives.Items;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment
{
    public class SpasEquipment : MonoBehaviour, IEquipment, IAllowInfiniteAmmo
    {
        [Header("Emitters")]
        [SerializeField] private SerializedInterface<ISoundEmitter> _gunShotSoundMono;
        private ISoundEmitter _gunShotSound => _gunShotSoundMono.Interface;

        public ItemType EquipmentType => ItemType.Shotgun;

        public EquipmentStats Stats { get; private set; }


        // How many rounds are in your current magazine
        public int RoundCount => _magazine.RoundCount;

        public event Action<int> OnFire;
        public event Action<int, Action<int>> OnReload;

        private IMagazine _magazine;
        private bool _weaponRaised;
        private Vector2 _aimPos;
        private Vector2 _playerPos => new Vector2(transform.position.x, transform.position.y);

        [Header("Cheats")]
        [SerializeField] private bool _hasInfiniteAmmo;
        public bool HasInfiniteAmmo => _hasInfiniteAmmo;

        public int Priority => 0;


        private IGameContext _gameContext;
        private ProjectileStats _projectileStats;
        private IAudioService _audioService;

        public void Init(EquipmentStats equipmentStats, IGameContext gameContext)
        {
            Stats = equipmentStats;
            _magazine = new Magazine(equipmentStats.MagazineSize);
            _projectileStats = Stats.Projectile;
            _gameContext = gameContext;
            _audioService = gameContext.AudioService;
            gameObject.GetComponentInChildren<IInitStats<EquipmentStats>>().Init(Stats);

#if !UNITY_EDITOR
            _hasInfiniteAmmo = false;
#endif
        }

        public void Aim(Vector2 mosPos)
        {
            // Draw a cross hair
            _aimPos = Camera.main.ScreenToWorldPoint(mosPos);
        }

        public void Fire()
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
            OnFire?.Invoke(_magazine.RoundCount);

            _audioService.PlaySFX(
                new LevelObjectSoundRequest(
                    LevelObjectEntityKey.Shotgun,
                    ActionSoundKey.Attack));

            _gunShotSound.EmitSound();
            yield return new WaitForSeconds(Stats.FireRate);
            _audioService.PlaySFX(
                new LevelObjectSoundRequest(
                    LevelObjectEntityKey.Shotgun,
                    ActionSoundKey.PickUp));
            yield return new WaitForSeconds(0.5f);
            // After firing, wait then reload
        }

        public void RaiseWeapon(bool raiseWeapon)
        {
            _weaponRaised = raiseWeapon;
            if (_weaponRaised)
            {

            }
            else
            {

            }
        }
        public void RequestReload()
        {
            int requestAmount = _magazine.Capacity - _magazine.RoundCount;
            // Debug.Log($"Requesting: {requestAmount} rocks");
            if (_hasInfiniteAmmo)
            {
                _magazine.ReplenishRounds(_magazine.Capacity);
            }
            else
            {
                OnReload?.Invoke(requestAmount, _magazine.ReplenishRounds);
            }
        }

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
    }
}