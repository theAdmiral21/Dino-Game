namespace Game.Core.State.Services
{
    public interface IGameStateServices
    {
        // Gets the current game state
        public IGameStateProvider GameState { get; }
        // Requests a change to the current game state
        public IChangeGameStateService ChangeGameState { get; }
        // Game state events
        public IGameStateEvents GameStateEvents { get; }
    }
}