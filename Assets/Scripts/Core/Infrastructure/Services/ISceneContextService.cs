using System.Runtime.CompilerServices;

namespace Infrastructure.Core.Services
{
    public interface ISceneContextService : IOverworldPositionContext
    {
        public bool TryGetContext<T>(out T context) where T : class;
        public void Clear();

    }
}