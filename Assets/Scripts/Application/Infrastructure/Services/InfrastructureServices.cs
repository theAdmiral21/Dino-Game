using Infrastructure.Core.Services;
using Primitives.Common.Infrastructure;
using Primitives.Infrastructure;

namespace Infrastructure.Application
{
    public class InfrastructureServices : IInfrastructureServices
    {
        public IRequestQuit RequestQuit { get; private set; }

        public IQuitExecutor ExecuteQuit { get; private set; }

        public InfrastructureServices(IQuitApprover gameStateManager, IQuitExecutor quitExecutor)
        {
            ExecuteQuit = quitExecutor;
            RequestQuit = new RequestQuitService(gameStateManager, ExecuteQuit);
        }
    }
}