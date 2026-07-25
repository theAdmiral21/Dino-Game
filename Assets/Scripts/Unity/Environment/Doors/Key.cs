using Core.Environment.Events;
using Core.Environment.Interactions;
using Game.Unity.Events;
using Physics.Core.PhysicsActors;
using Primitives.Environment;
using Unity.Common.Pickups;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Environment.Doors
{
    public class Key : Pickup, IKey
    {
        public KeyId KeyId => _id;
        [SerializeField] private KeyId _id;

        [SerializeField] private AudioFeedBack _pickUpSound;

        public override void OnPickup(IPhysicsActor entity)
        {
            // Unlock all the doors that use this key on pick up
            _eventBus.Publish(new UnlockEvent { Id = KeyId });
            _pickUpSound.React();
            Destroy(gameObject);
        }
    }
}