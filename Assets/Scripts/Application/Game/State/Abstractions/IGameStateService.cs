using System;
using Game.Core.State.Services;
using Primitives.GameState;

namespace Game.Application.State.Abstractions
{
    public interface IGameStateService : IGameStateProvider
    {
        public event Action<GameState> OnGameStateChanged;

    }
}