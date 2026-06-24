namespace AI.Core.State
{
    public interface IDieContext
    {
        public bool CheckIsAlive();

        // Note that responding to death events is handled by subscribing to the OnDeath event in the health component.
    }
}