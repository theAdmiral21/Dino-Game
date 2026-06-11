using Game.Core.State.Services;
using Primitives.GameState;

namespace Game.Application.State.Services
{
    public class ChangeGameStateService : IChangeGameStateService
    {
        private readonly GameStateManager _gameStateManager;

        public ChangeGameStateService(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public void ChangeGameState(GameState newState)
        {
            _gameStateManager.RequestStateChange(newState);
        }
    }
}