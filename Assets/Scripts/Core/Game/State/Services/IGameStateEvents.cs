using System;
using Primitives.GameState;

namespace Game.Core.State.Services
{
    public interface IGameStateEvents : IGameStateProvider
    {
        public event Action<GameState> OnGameStateChanged;

    }
}