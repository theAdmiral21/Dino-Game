using System;
using Game.Core.State.Services;
using Primitives.GameState;

namespace Game.Application.State.Services
{
    public class GameStateEventService : IGameStateEvents
    {
        public GameState CurrentState => _gameStateManager.CurrentState;
        private GameStateManager _gameStateManager;

        public GameStateEventService(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public event Action<GameState> OnGameStateChanged
        {
            add => _gameStateManager.OnGameStateChanged += value;
            remove => _gameStateManager.OnGameStateChanged -= value;
        }
    }
}