using Primitives.Common.Infrastructure;
using Primitives.Infrastructure;

namespace Infrastructure.Application
{
    public class RequestQuitService : IRequestQuit
    {
        private IQuitApprover _gameStateManager;
        private IQuitExecutor _quitExecutor;
        public RequestQuitService(IQuitApprover gameStateManager, IQuitExecutor quitExecutor)
        {
            _gameStateManager = gameStateManager;
            _quitExecutor = quitExecutor;
        }
        public void RequestQuit()
        {
            if (_gameStateManager.CanQuit())
            {
                _quitExecutor.Quit();
            }
        }
    }
}