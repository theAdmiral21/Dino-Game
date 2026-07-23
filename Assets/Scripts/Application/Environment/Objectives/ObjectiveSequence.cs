using System;
using System.Collections.Generic;
using Core.Environment.Objectives;

namespace Application.Environment.Objectives
{
    public class ObjectiveSequence : IObjectiveSequence
    {
        public event Action<IObjective> OnObjectiveCompleted;
        public event Action OnSequenceComplete;

        private readonly List<IObjective> _objectives;
        private int _currentNdx;

        public ObjectiveSequence(List<IObjective> objectives)
        {
            _objectives = objectives;
        }

        public void StartMission()
        {
            _currentNdx = 0;
            ActivateCurrent();
        }

        private void ActivateCurrent()
        {
            // sub to the new objective
            _objectives[_currentNdx].OnComplete += HandleComplete;
            // start the current objective
            _objectives[_currentNdx].Activate();
        }

        private void HandleComplete(IObjective objective)
        {
            // unsub from the objective
            objective.OnComplete -= HandleComplete;
            // clean up objective
            objective.Deactivate();
            // raise objective completed
            OnObjectiveCompleted?.Invoke(objective);
            // increment ndx
            _currentNdx += 1;

            // check if the mission is complete
            if (_currentNdx >= _objectives.Count)
            {
                OnSequenceComplete?.Invoke();
            }
            else
            {
                // activate next objective
                ActivateCurrent();
            }
        }
    }
}