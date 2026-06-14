using System;
using Application.Inventory;
using Core.Equipment;
using Core.Inventory;
using Core.Inventory.DataStructures.Consumers;
using Primitives.Items;
using UnityEngine;

namespace Unity.Equipment
{
    public class RockEquipment : MonoBehaviour, IEquipment
    {
        public ItemType EquipmentType { get; private set; }

        public EquipmentStats Stats { get; private set; }

        public IInventorySystem InventorySystem { get; private set; }

        // For rocks, the round count is how many TOTAL rocks you have
        public int RoundCount => _magazine.RoundCount;


        public event Action OnInventoryEmpty;
        public event Action OnFire;
        public event Action OnReload;

        private IMagazine _magazine;

        public RockEquipment(ItemType item, EquipmentStats stats, IInventorySystem system)
        {
            EquipmentType = item;
            Stats = stats;
            InventorySystem = system;
            _magazine = new Magazine(item, stats.MagazineSize);
        }

        public void Aim()
        {
            // Draw a cross hair

            // Draw an arc from the player to the cross hair, is that too easy?
        }

        public void Fire()
        {
            // try to consume a rock
            var bullet = _magazine.ConsumeItem(new RockConsumer());
            // if not null, throw the rock
            if (bullet != null)
            {
                Debug.Log($"Throwing rock!");
            }
        }

        public void RaiseWeapon()
        {
            // if you have rocks

            // cock your arm back

            // allow aiming
        }

        public void Reload()
        {
            // Do throwables actually reload? Or is this more or less decoration? Because you need a way to replace the rock you just threw but you don't have a magazine unless you count the fact that you can hold one rock at a time. That could be your magazine. So maybe there are 2 inventories? OH OR A MAGAZINE CLASS THAT IS FILLED WITH PROVIDER REQUESTS!

            // Try and get a rock from your inventory
            var bulletProvider = InventorySystem.ConsumeItem(new RockConsumer());
            // If the rock is null don't do anything
            if (bulletProvider == null) return;
            // If you got a rock, try to add it to your magazine
            if (!_magazine.AddItem(bulletProvider))
            {
                // if you fail, return it to your inventory
                InventorySystem.Items[EquipmentType].AddItem(bulletProvider);
            }
        }
    }
}