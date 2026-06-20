using System;
using System.Collections;
using Application.Inventory;
using Core.Equipment;
using Core.Inventory.DataStructures.Consumers;
using Primitives.Items;
using UnityEditor;
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

        public void Init(EquipmentStats stats)
        {
            Stats = stats;
            _magazine = new Magazine(stats.MagazineSize);
            Debug.Log($"Rock initialized");
        }

        public void Aim(Vector2 mosPos)
        {
            // Draw a cross hair

            // Draw an arc from the player to the cross hair, is that too easy?
            Debug.Log($"Aiming rock!");

            // Draw a line from the equipment to the cursor
            Debug.DrawLine(Vector2.zero, mosPos);

        }

        public void Fire()
        {
            Debug.Log($"Attempting to throw rock!");
            // try to consume a rock
            if (_magazine.ConsumeRound())
            {
                Debug.Log($"Rock fired!");
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

        public void RaiseWeapon()
        {
            Debug.Log($"Raising rock!");
            // if you have rocks

            // Other wise reload
            if (_magazine.RoundCount == 0)
            {
                RequestReload();
            }

            // cock your arm back

            // allow aiming
        }

        public void RequestReload()
        {
            int requestAmount = _magazine.Capacity - _magazine.RoundCount;
            Debug.Log($"Requesting: {requestAmount} rocks");
            OnReload?.Invoke(requestAmount, _magazine.ReplenishRounds);
        }


    }
}