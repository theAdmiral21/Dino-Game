using Movement.Core.Abstractions;

namespace Movement.Unity.Abstractions
{
    public interface IStatProvider
    {
        public IStatSheet StatSheet { get; }
        // public PlayerStats Stats { get; }
        // public void SetStats(ScriptableObject statSO);
    }
}