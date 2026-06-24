namespace AI.Core.State.BehaviorContext
{
    public interface ISwitchContext
    {
        public bool IsActive { get; }
        public void Switch();
    }

}
