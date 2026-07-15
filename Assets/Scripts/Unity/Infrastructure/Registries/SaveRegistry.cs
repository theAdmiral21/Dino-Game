using System.Collections.Generic;
using Core.Game.Lifecycle;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Unity.Infrastructure.Registries
{
    public class SaveRegistry : MonoBehaviour, ISaveRegistry
    {
        [Header("Resetables")]
        [SerializeField]
        private Registry<IResetable> _resetables = new();
        public IReadOnlyCollection<IResetable> Resetables => _resetables.Entities;

        // [Header("SnapShotables")]
        // [SerializeField]
        // private Registry<ISnapShotable> _snapShotables = new();
        // public IReadOnlyCollection<ISnapShotable> SnapShotables => _snapShotables.Entities;

        [Header("Save Orchestrators")]
        [SerializeField]
        private Registry<ISaveOrchestrator> _saveOrchestrators = new();
        public IReadOnlyCollection<ISaveOrchestrator> SaveOrchestrators => _saveOrchestrators.Entities;

        private void Awake()
        {
            RegistryGateway.SetRegistry(_resetables);
            // RegistryGateway.SetRegistry(_snapShotables);
            RegistryGateway.SetRegistry(_saveOrchestrators);
        }
    }
}