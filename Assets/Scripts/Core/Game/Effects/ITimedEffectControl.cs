using System;

namespace Game.Core.Effects
{
    public interface ITimedEffectControl
    {
        public event Action OnStarted;
        public event Action OnWindingDown;
        public event Action OnEnded;

        public void Start(float duration);
        public void Extend(float duration);
        public void Tick(float dt);
    }
}