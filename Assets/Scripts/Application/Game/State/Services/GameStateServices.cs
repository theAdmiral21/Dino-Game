using Game.Core.State.Services;

namespace Game.Application.State.Services
{
    public class GameStateServices : IGameStateServices
    {
        public IGameStateProvider GameState => _gameState;
        private IGameStateProvider _gameState;
        public IChangeGameStateService ChangeGameState => _changeGameState;
        private IChangeGameStateService _changeGameState;

        public IGameStateEvents GameStateEvents => _gameStateEvents;
        private IGameStateEvents _gameStateEvents;

        public GameStateServices(GameStateManager gameStateManager)
        {
            _gameStateEvents = new GameStateEventService(gameStateManager);
            _changeGameState = new ChangeGameStateService(gameStateManager);
            _gameState = new GameStateProviderService(gameStateManager);
        }
    }
}