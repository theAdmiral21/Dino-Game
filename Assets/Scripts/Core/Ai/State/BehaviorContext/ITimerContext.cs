namespace AI.Core.State.BehaviorContext
{
    public interface ITimerContext
    {
        public float TotalTime { get; }
        public float Counter { get; }
        public bool TimerComplete { get; }
        public bool IsActive { get; }
        public void TickTimer(float dt);
        public void ResetTimer();
        public void StartTimer();
        public void ResumeTimer();
        public void PauseTimer();
        public void StopTimer();
    }

}
