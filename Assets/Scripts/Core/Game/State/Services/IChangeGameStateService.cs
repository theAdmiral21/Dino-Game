using Primitives.GameState;

namespace Game.Core.State.Services
{
    public interface IChangeGameStateService
    {
        public void ChangeGameState(GameState newState);

    }
}