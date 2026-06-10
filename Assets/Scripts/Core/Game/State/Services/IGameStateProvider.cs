using Primitives.GameState;

namespace Game.Core.State.Services
{
    public interface IGameStateProvider
    {
        public GameState CurrentState { get; }
    }
}