using Core.Inventory;
using Core.Inventory.DataStructures.Providers;
using Physics.Core.PhysicsActors;
using Unity.Common.Pickups;
using UnityEngine;

namespace Unity.Common.PickUps
{
    public class RockPickUp : Pickup
    {
        [SerializeField] private int _quantity;
        public override void OnPickup(IPhysicsActor entity)
        {
            Debug.Log($"PickUp called");
            // Get the inventory component
            IInventory inventory = entity.GetComponentInChildren<IInventory>();

            // Attempt to add yourself
            if (inventory.InventorySystem.AddItem(new RockProvider(_quantity)))
            {
                // Destroy the pick up if you succeeded
                Destroy(gameObject);
            }
            // If you fail don't do anything

        }
    }
}