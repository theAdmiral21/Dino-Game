using Core.Detection.Audio;
using Core.Equipment;
using Core.Physics.Collision.Callbacks;
using Core.Physics.Collisions;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Primitives.Audio;
using Primitives.Items;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Equipment.Throwables
{
    public class ThrowableContactHandler : MonoBehaviour, ICollisionHandler, ICollisionEnterEvent
    {
        [SerializeField] private SerializedInterface<ISoundEmitter> _soundEmitterMono;
        private ISoundEmitter _soundEmitter => _soundEmitterMono.Interface;
        public void OnCollisionEntered(CollisionInfo collision)
        {
            Debug.Log($"Got collision entered");

            _soundEmitter.EmitSound();
        }



    }
}