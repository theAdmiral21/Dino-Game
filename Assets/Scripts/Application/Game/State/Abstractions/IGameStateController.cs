using Game.Core.State.Services;
using Primitives.GameState;

namespace Game.Application.State.Services
{
    public interface IGameStateController : IGameStateProvider
    {
        public void RequestStateChange(GameState request);
    }
}