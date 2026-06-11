using Infrastructure.Core.Lifecycle;
using Infrastructure.Core.Services;

namespace Infrastructure.Application.Services
{
    public class PlayerServices : IPlayerServices
    {
        public ISpawnPlayer SpawnPlayer { get; private set; }

        public IPlayerProvider PlayerProvider { get; private set; }
        public IDestroyPlayer DestroyPlayer { get; private set; }

        public PlayerServices(ISpawnPlayer spawnPlayer, IPlayerProvider playerProvider, IDestroyPlayer destroyPlayer)
        {
            SpawnPlayer = spawnPlayer;
            PlayerProvider = playerProvider;
            DestroyPlayer = destroyPlayer;
        }
    }
}