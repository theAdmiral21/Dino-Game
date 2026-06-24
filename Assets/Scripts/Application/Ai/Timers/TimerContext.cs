using UnityEngine;
using AI.Core.State.BehaviorContext;

namespace AI.Application.Timers
{
    public class TimerContext : ITimerContext
    {

        public float TotalTime { get; private set; }

        public float Counter { get; private set; }

        public bool TimerComplete { get; private set; }

        public bool IsActive { get; private set; }

        private float _waitTime;
        public TimerContext(float waitTime)
        {
            _waitTime = waitTime;
            TotalTime = _waitTime;
            IsActive = false;
        }
        public void ResetTimer()
        {
            PauseTimer();
            Counter = TotalTime;
            TimerComplete = false;
        }


        public void TickTimer(float dt)
        {
            if (!IsActive) return;
            if (Counter > 0)
            {
                Counter -= dt;
            }
            if (Counter <= 0)
            {
                TimerComplete = true;
                IsActive = false;
            }
        }

        public void PauseTimer()
        {
            IsActive = false;
        }

        public void StopTimer()
        {
            IsActive = false;
            ResetTimer();
        }

        public void StartTimer()
        {
            IsActive = true;
        }

        public void ResumeTimer()
        {
            StartTimer();
        }
    }
}