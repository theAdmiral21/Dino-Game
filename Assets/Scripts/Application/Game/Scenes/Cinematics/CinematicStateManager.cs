using System;
using Game.Core.Cinematics.Enums;
using UnityEngine;

namespace Game.Application.Cinematics
{
    public class CinematicStateManager
    {
        public CinematicState CurrentState => _currentState;
        private CinematicState _currentState;

        public event Action<CinematicState> OnCinematicStatusChanged;

        public CinematicStateManager()
        {
            _currentState = CinematicState.None;
        }

        public void UpdateState(CinematicState newState)
        {
            Debug.Log($"Cinematic state changed from {_currentState} to {newState}");
            _currentState = newState;
            OnCinematicStatusChanged?.Invoke(_currentState);
        }
    }
}