using System;
using System.Collections.Generic;
using System.Linq;
using Application.Environment.Objectives;
using Core.Environment.Objectives;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Environment.Objectives
{
    public class ObjectiveOrchestrator : MonoBehaviour, IObjectiveSequence
    {
        [SerializeField] private List<SerializedInterface<IObjective>> _objectivesMono;
        private List<IObjective> _objectives;
        private IObjectiveSequence _sequence;

        public event Action<IObjective> OnObjectiveCompleted;
        public event Action OnSequenceComplete;

        private void Awake()
        {
            _objectives = _objectivesMono.Select(x => x.Interface).ToList();
            _sequence = new ObjectiveSequence(_objectives);

            // is this ghetto as hell?
            _sequence.OnObjectiveCompleted += EmitObjectiveCompleted;
            _sequence.OnSequenceComplete += EmitSequenceCompleted;
        }
        public void StartMission() => _sequence.StartMission();

        private void EmitSequenceCompleted() => OnSequenceComplete?.Invoke();

        private void EmitObjectiveCompleted(IObjective objective) => OnObjectiveCompleted?.Invoke(objective);



        [ContextMenu("Test Generators Complete")]
        private void TestGeneratorObjectiveComplete()
        {
            OnObjectiveCompleted?.Invoke(_objectives[0]);
        }
        [ContextMenu("Test Water Complete")]
        private void TestWaterObjectiveComplete()
        {
            OnObjectiveCompleted?.Invoke(_objectives[1]);
        }
        [ContextMenu("Test Steam 1 Complete")]
        private void TestSteam2ObjectiveComplete()
        {
            OnObjectiveCompleted?.Invoke(_objectives[2]);
        }
        [ContextMenu("Test Steam 2 Complete")]
        private void TestSteam1ObjectiveComplete()
        {
            OnObjectiveCompleted?.Invoke(_objectives[3]);
        }
        [ContextMenu("Test Turbine Complete")]
        private void TestTurbineObjectiveComplete()
        {
            OnObjectiveCompleted?.Invoke(_objectives[4]);
        }
    }
}