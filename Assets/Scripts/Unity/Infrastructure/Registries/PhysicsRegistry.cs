using System.Collections.Generic;
using Gameplay.Common.Application.Abstractions;
using Physics.Core.Abstractions;
using Physics.Core.PhysicsActors;
using UnityEngine;

namespace Infrastructure.Unity.Registries
{
    public class PhysicsRegistry : MonoBehaviour, IPhysicsRegistry
    {
        [Header("Debug")]
        [SerializeField]
        private Registry<IPhysicsActor> _actors = new();
        public IReadOnlyCollection<IPhysicsActor> Actors => _actors.Entities;


        public IReadOnlyCollection<ITriggerVolume> Triggers => _triggers.Entities;
        private Registry<ITriggerVolume> _triggers = new();

        public void Awake()
        {
            RegistryGateway.SetRegistry(_actors);
            RegistryGateway.SetRegistry(_triggers);
        }
    }
}