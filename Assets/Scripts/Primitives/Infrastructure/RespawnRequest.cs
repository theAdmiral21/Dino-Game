using Primitives.Characters;

namespace Primitives.Common.Infrastructure.DataStructures
{
    public struct RespawnRequest
    {
        public int PlayerId { get; private set; }

        public int SpawnIndex { get; private set; }

        public CharacterID Id { get; private set; }

        public RespawnRequest(int playerId, int spawnIndex, CharacterID id)
        {
            PlayerId = playerId;
            SpawnIndex = spawnIndex;
            Id = id;
        }
    }
}