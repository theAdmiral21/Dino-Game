using System;
using Core.Environment.Objectives;
using Gameplay.Common.Unity;
using Physics.Core.PhysicsActors;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Environment.Ambience
{
    public class MissionStartTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerVolume _triggerCollider;

        [SerializeField] private SerializedInterface<IObjectiveSequence> _objectiveOrchestratorMono;
        private IObjectiveSequence _objectiveOrchestrator => _objectiveOrchestratorMono.Interface;

        public void Awake()
        {
            _triggerCollider.OnVolumeEntered += StartMission;
        }

        public void OnDestroy()
        {
            if (_triggerCollider != null) _triggerCollider.OnVolumeEntered -= StartMission;
        }

        private void StartMission(IPhysicsActor actor)
        {
            _objectiveOrchestrator.StartMission();
        }
    }
}