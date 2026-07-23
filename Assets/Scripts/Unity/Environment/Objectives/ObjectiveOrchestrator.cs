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



        [ContextMenu("Test Generator Complete")]
        private void TestGeneratorObjectiveComplete()
        {
            OnObjectiveCompleted?.Invoke(_objectives[0]);
        }
    }
}