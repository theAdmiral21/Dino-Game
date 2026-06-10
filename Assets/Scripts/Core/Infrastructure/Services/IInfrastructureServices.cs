using Primitives.Common.Infrastructure;

namespace Infrastructure.Core.Services
{
    public interface IInfrastructureServices
    {
        public IRequestQuit RequestQuit { get; }
        public IQuitExecutor ExecuteQuit { get; }
    }
}