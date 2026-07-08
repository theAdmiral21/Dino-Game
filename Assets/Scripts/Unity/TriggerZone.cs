using System;
using Core.Physics.Triggers;
using Infrastructure.Unity;
using Physics.Core.Abstractions;
using UnityEngine;

namespace Gameplay.Common.Unity
{
    /// <summary>
    /// Helper component for trigger detection zones.
    /// Forwards trigger events to parent EnemyController.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TriggerZone : MonoBehaviour, ITriggerVolume
    {
        [SerializeField] protected bool IsOneShot;

        public EntityId Id => GetEntityId();

        public Collider2D Collider { get; private set; }

        public IBoundsProvider BoundsProvider { get; private set; }

        public event Action<GameObject> OnPlayerEnter;
        private void Awake()
        {
            Collider = GetComponent<Collider2D>();
            RegistryGateway.Register<ITriggerVolume>(this);
        }
        private void OnDestroy()
        {
            RegistryGateway.Deregister<ITriggerVolume>(this);
        }

    }
}