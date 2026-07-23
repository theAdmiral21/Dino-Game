using System;
using Primitives.Environment.Platforms;

namespace Core.Environment.Objectives
{
    public interface IObjective
    {
        public ObjectiveId Id { get; }
        public event Action<IObjective> OnComplete;
        public void TryComplete();
        public void Activate();
        public void Deactivate();
    }
}