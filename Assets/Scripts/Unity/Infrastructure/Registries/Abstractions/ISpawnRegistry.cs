using System.Collections.Generic;
using Environment.Core.Level;
using Infrastructure.Core.Registries;

namespace Infrastructure.Unity.Registries
{
    public interface ISpawnRegistry
    {
        public IReadOnlyCollection<ISpawnPoint> SpawnPoints { get; }

    }
}