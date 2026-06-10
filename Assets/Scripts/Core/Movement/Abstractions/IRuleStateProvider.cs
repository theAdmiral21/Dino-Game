using Movement.Core.Abstractions;

namespace Core.Movement.Abstractions
{
    public interface IRuleStateProvider
    {
        public IRuleState RuleStateView { get; }
    }
}