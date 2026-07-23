using System;
using System.Collections.Generic;
using Core.Environment.Ambience;
using Core.Environment.Objectives;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Unity.Environment.Objectives;
using UnityEngine;

namespace Unity.Environment.Ambience
{
    public class AmbienceManager : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private ObjectiveOrchestrator _missionObjectives;
        [SerializeField] private List<AmbienceCue> _cues;

        [SerializeField] private int _priority;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            _missionObjectives.OnObjectiveCompleted += HandleObjectiveComplete;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_missionObjectives != null, $"You forgot to assign the mission objectives orchestrator.");
        }

        private void HandleObjectiveComplete(IObjective objective)
        {
            for (int i = 0; i < _cues.Count; i++)
            {
                if (_cues[i].TriggerObjective == objective.Id)
                {
                    for (int j = 0; j < _cues[i].Effects.Count; j++)
                    {
                        _cues[i].Effects[j].Interface.Perform();
                    }
                }
            }
        }
    }
}