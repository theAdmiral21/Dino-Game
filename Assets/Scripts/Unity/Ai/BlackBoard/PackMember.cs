using System;
using Core.Ai.BlackBoard;
using Core.Detection;
using Core.Game.HealthSystem.Health;
using NPC.Application.BehaviorContexts;
using Unity.Common;
using Unity.Common.Unity;
using Unity.Infrastructure.Providers;
using Unity.NPC.Controllers;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Unity.Ai.BlackBoard
{
    public class PackMember : MonoBehaviour, IPackMember
    {
        public int MemberId => GetInstanceID();
        public MemberStatus Status { get; private set; } = new();

        [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        private IHealthComponentProvider _healthComponent => _healthComponentMono.Interface;

        [SerializeField] private SerializedInterface<IDetectorOrchestrator> _detectorOrchestratorMono;
        private IDetectorOrchestrator _detectorOrchestrator => _detectorOrchestratorMono.Interface;

        [SerializeField] private RaptorController _raptorController;
        private RaptorContext _context => _raptorController.Context;

        [SerializeField] private SerializedInterface<IRaptorController> _raptorControllerMono;
        public IRaptorController RaptorController => _raptorControllerMono.Interface;

        public IPackManager PackManager { get; private set; }

        [Header("Debug")]
        [SerializeField] private bool _debugMemberStatus;
        [SerializeField] private MemberStatus _debugStatus;
        public void UpdateMemberStatus()
        {
            Status.Health = _healthComponent.HealthComponent.StateOfHealth;
            Status.Position = transform.position;
            Status.CurrentStatus = _context.CurrentStatus;
            Status.Alertness = _detectorOrchestrator.DetectorBrain.Alertness;
            Status.Perception = _context.Perception;

            if (_debugMemberStatus)
            {
                _debugStatus = Status;
            }
        }
        public void SetPackManager(IPackManager packManager)
        {
            PackManager = packManager;
            // This should only happen once or rarely
            UpdateRaptorPackManager();
        }

        private void UpdateRaptorPackManager()
        {
            var provider = ProviderLookUp.Require<RaptorDataProvider>(this);
            provider.SetPackDataProvider(PackManager);
        }
    }
}