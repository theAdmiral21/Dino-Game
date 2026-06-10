using Movement.Core.Stats;

namespace Movement.Core.Abstractions
{
    public interface IStatSheet
    {
        public IStatCollection StatCollection { get; }
    }
}