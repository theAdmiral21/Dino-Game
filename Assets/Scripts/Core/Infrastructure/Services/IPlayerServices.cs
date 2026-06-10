using Infrastructure.Core.Lifecycle;

namespace Infrastructure.Core.Services
{
    public interface IPlayerServices
    {
        public ISpawnPlayer SpawnPlayer { get; }
        public IPlayerProvider PlayerProvider { get; }
        public IDestroyPlayer DestroyPlayer { get; }
    }
}