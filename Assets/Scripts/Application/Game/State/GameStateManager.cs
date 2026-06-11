using System;
using Game.Application.State.Services;
using Game.Core.State.Services;
using Primitives.GameState;
using Primitives.Infrastructure;
using UnityEngine;

namespace Game.Application.State
{
    /// <summary>
    /// Class for controlling the current state of the game. GameStateManager handle state change requests and quit requests for the game. Systems can interact with the GameStateManager via the GameStateServices service.
    /// </summary>
    public class GameStateManager : IGameStateController, IGameStateEvents, IQuitApprover
    {
        public GameState CurrentState => _currentState;
        private GameState _currentState;
        private GameStatePolicy _statePolicy = new GameStatePolicy();
        public event Action<GameState> OnGameStateChanged;



        public void RequestStateChange(GameState newState)
        {
            if (_statePolicy.CanChange(_currentState, newState))
            {
                Debug.Log($"Changed game state from {_currentState} to {newState}");
                SetGameState(newState);
            }
        }

        private void SetGameState(GameState newState)
        {
            _currentState = newState;
            OnGameStateChanged?.Invoke(_currentState);
        }

        public bool CanQuit()
        {
            // Evaluate if we can quit the game
            if (_currentState == GameState.Paused) return true;
            if (_currentState == GameState.InMenu) return true;
            return false;
        }
    }
}