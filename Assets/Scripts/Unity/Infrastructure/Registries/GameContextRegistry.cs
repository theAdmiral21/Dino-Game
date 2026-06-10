using System;
using Game.Core.Execution;

namespace Infrastructure.Unity
{
    public static class GameContextRegistry
    {
        public static IGameContext GameContext { get; private set; }
        public static void Set(IGameContext context)
        {
            if (GameContext != null)
            {
                throw new InvalidOperationException("GameContext has already been set.");
            }
            GameContext = context;
        }
    }
}