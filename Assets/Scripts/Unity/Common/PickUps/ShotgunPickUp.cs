using Core.Inventory;
using Core.Inventory.DataStructures.Providers;
using Game.Core.Events;
using Game.Unity.Events;
using Physics.Core.PhysicsActors;
using Unity.Common.Pickups;
using UnityEngine;

namespace Unity.Common.PickUps
{
    public class ShotgunPickUp : Pickup
    {
        private IEventFeedBack _audio;
        private void Awake()
        {
            base.Awake();
            _audio = GetComponent<IEventFeedBack>();
        }
        public override void OnPickup(IPhysicsActor entity)
        {
            Debug.Log($"PickUp called");
            // Get the inventory component
            IInventory inventory = entity.GetComponentInChildren<IInventory>();

            // Attempt to add yourself
            if (inventory.InventorySystem.AddItem(new ShotgunProvider(1)))
            {
                // Play a sound
                if (_audio != null) _audio.React();
                // Destroy the pick up if you succeeded
                Destroy(gameObject);
            }
            // If you fail don't do anything

        }
    }
}