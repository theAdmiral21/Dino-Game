using Primitives.Items;
using Core.Inventory;
using Core.Inventory.DataStructures.Providers;
using Core.Inventory.Requests;
using Game.Core.Events;
using Physics.Core.PhysicsActors;
using Unity.Common.Pickups;
using UnityEngine;
using Unity.Common.Unity;

namespace Unity.Common.PickUps
{
    public class ItemPickUp : Pickup
    {
        [Header("Type of item provided")]
        [SerializeField] private ItemType Item;
        [Header("Amount of item provided")]
        [SerializeField] private int _quantity;

        [Header("Optional Feedback")]
        [SerializeField] private SerializedInterface<IEventFeedBack> _audioMono;
        [SerializeField] private SerializedInterface<IEventFeedBack> _visualMono;
        [SerializeField] private SerializedInterface<IEventFeedBack> _particleMono;
        IEventFeedBack _audio => _audioMono.Interface;
        IEventFeedBack _visual => _visualMono.Interface;
        IEventFeedBack _particle => _particleMono.Interface;

        private IItemProviderRequest _providedItem;
        private void Awake()
        {
            base.Awake();
            _providedItem = BuildProvider();
        }
        private IItemProviderRequest BuildProvider()
        {
            return Item switch
            {
                ItemType.Rock => new RockProvider(_quantity),
                ItemType.Canister => new CanisterProvider(_quantity),
                ItemType.Taser => new TaserProvider(_quantity),
                ItemType.Shotgun => new ShotgunProvider(_quantity),
                ItemType.RocketLauncher => new RocketLauncherProvider(_quantity),
                ItemType.NerveGas => new NerveGasProvider(_quantity),
                ItemType.Shell => new ShellProvider(_quantity),
                ItemType.Rocket => new RocketProvider(_quantity),
                ItemType.Flashlight => new FlashlightProvider(_quantity),
                ItemType.Medkit => new MedkitProvider(_quantity),
                ItemType.Flares => new FlareProvider(_quantity),
                ItemType.SmokeGrenade => new SmokeGrenadeProvider(_quantity),
            };
        }
        public override void OnPickup(IPhysicsActor entity)
        {
            Debug.Log($"PickUp called");
            // Get the inventory component
            IInventory inventory = entity.GetComponentInChildren<IInventory>();
            Debug.Log($"Providing {_providedItem.Quantity} {_providedItem.Item}s");
            int deposited = inventory.StockItem(_providedItem);
            _quantity -= deposited;
            // Attempt to add yourself
            if (deposited > 0)
            {
                // Play a sound
                if (_audio != null) _audio.React();
                if (_visual != null) _visual.React();
                if (_particle != null) _particle.React();

            }

            if (_quantity <= 0)
            {
                Destroy(gameObject);
            }

        }
    }
}