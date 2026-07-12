using Primitives.Health;

namespace Core.Game
{
    public record OnHealthChanged
    {
        public int HealthAmount;
        public HealthState State;
    }
}