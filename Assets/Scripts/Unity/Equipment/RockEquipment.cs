using System;
using Application.Inventory;
using Core.Equipment;
using Core.Inventory.DataStructures.Consumers;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment
{
    public class RockEquipment : MonoBehaviour, IEquipment
    {
        public ItemType EquipmentType => ItemType.Rock;

        public EquipmentStats Stats { get; private set; }


        // How many rounds are in your current magazine
        public int RoundCount => _magazine.RoundCount;


        public event Action OnFire;
        public event Action OnReload;

        private IMagazine _magazine;

        public RockEquipment(EquipmentStats stats)
        {

            Stats = stats;
            _magazine = new Magazine(stats.MagazineSize);
        }

        public void Aim()
        {
            // Draw a cross hair

            // Draw an arc from the player to the cross hair, is that too easy?
            Debug.Log($"Aiming rock!");
        }

        public void Fire()
        {
            Debug.Log($"Attempting to throw rock!");
            // try to consume a rock
            if (_magazine.ConsumeRound())
            {
                Debug.Log($"Rock fired!");
            }
        }

        public void RaiseWeapon()
        {
            Debug.Log($"Raising rock!");
            // if you have rocks

            // Other wise reload
            if (_magazine.RoundCount == 0)
            {
                Reload();
            }

            // cock your arm back

            // allow aiming
        }

        public void Reload()
        {

        }
    }
}