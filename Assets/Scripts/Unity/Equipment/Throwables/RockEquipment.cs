using System;
using System.Collections;
using System.Linq;
using Application.Inventory;
using Core.Equipment;
using Game.Core.Execution;
using Movement.Core.Movement.DataStructures;
using Physics.Core.PhysicsActors;
using Primitives.Items;
using Unity.Game.GameLoop;
using UnityEngine;

namespace Unity.Equipment
{
    public class RockEquipment : MonoBehaviour, IEquipment
    {
        [SerializeField] private GameObject _rockPrefab;
        public ItemType EquipmentType => ItemType.Rock;

        public EquipmentStats Stats { get; private set; }


        // How many rounds are in your current magazine
        public int RoundCount => _magazine.RoundCount;


        public event Action<int> OnFire;
        public event Action<int, Action<int>> OnReload;

        private IMagazine _magazine;

        private bool _weaponRaised;
        private Vector2 _aimPos;
        private Vector2 _playerPos => new Vector2(transform.position.x, transform.position.y);
        private IGameContext _gameContext;
        private ProjectileStats _projectileStats;

        public void Init(EquipmentStats equipmentStats, IGameContext gameContext)
        {
            Stats = equipmentStats;
            _magazine = new Magazine(equipmentStats.MagazineSize);
            _projectileStats = Stats.Projectile;
            _gameContext = gameContext;
            // Debug.Log($"Rock initialized");

        }

        public void Aim(Vector2 mosPos)
        {
            // Draw a cross hair

            // Draw an arc from the player to the cross hair, is that too easy?
            // Debug.Log($"Aiming rock!");

            // Draw a line from the equipment to the cursor
            _aimPos = Camera.main.ScreenToWorldPoint(mosPos);
            // Debug.Log($"mouse position: {mosPos}; mouse world position: {_aimPos}");
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

        public void Fire()
        {
            // Debug.Log($"Attempting to throw rock!");
            // try to consume a rock
            if (_magazine.ConsumeRound())
            {
                // Debug.Log($"Rock fired!");
                var rockObject = Instantiate(_rockPrefab);
                rockObject.GetComponentInChildren<IInitProjectile>().Init(_projectileStats);
                // hmm I have to initialize this entire thing before doing anything with it..
                var intializables = rockObject.GetComponentsInChildren<IInitializable<IGameContext>>();

                InitFactory.InitializeObject(_gameContext, intializables.ToList());
                // foreach (var init in intializables)
                // {
                //     init.Initialize(_gameContext);

                //     init.PostInitialize(_gameContext);
                // }
                rockObject.SetActive(false);

                rockObject.transform.position = transform.position;
                rockObject.TryGetComponent(out IPhysicsActor actor);
                rockObject.SetActive(true);
                Vector2 throwDirection = (_aimPos - _playerPos).normalized;
                actor.EnqueueActionRequest(new ExternalImpulseRequest(throwDirection * Stats.MuzzleVelocity, -10));
                // Debug.Log($"Throwing rock with velocity: {throwDirection * Stats.MuzzleVelocity}");
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
            // After firing, wait then reload
            yield return new WaitForSeconds(Stats.ReloadTime);
            RequestReload();
        }

        public void RaiseWeapon(bool raiseWeapon)
        {
            _weaponRaised = raiseWeapon;
            if (_weaponRaised)
            {
                // Debug.Log($"Raising rock!");
                // if you have rocks

                // Other wise reload
                if (_magazine.RoundCount == 0)
                {
                    RequestReload();
                }

                // cock your arm back

                // allow aiming
            }
            else
            {
                // lower the weapon
                // Debug.Log($"Lowering rock!");
            }
        }

        public void RequestReload()
        {
            int requestAmount = _magazine.Capacity - _magazine.RoundCount;
            // Debug.Log($"Requesting: {requestAmount} rocks");
            OnReload?.Invoke(requestAmount, _magazine.ReplenishRounds);
        }


    }
}