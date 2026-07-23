using System;

namespace Core.Environment.Objectives
{
    public interface IObjectiveSequence
    {
        public event Action<IObjective> OnObjectiveCompleted;
        public event Action OnSequenceComplete;

        public void StartMission();
    }
}