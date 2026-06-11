using Game.Core.State.Services;
using Primitives.GameState;

namespace Game.Application.State.Services
{
    public class GameStateProviderService : IGameStateProvider
    {
        public GameState CurrentState => _gameStateManager.CurrentState;
        private GameStateManager _gameStateManager;
        public GameStateProviderService(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }
    }
}