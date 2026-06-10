using Primitives.Characters;

namespace Primitives.Common.Infrastructure
{
    public interface IRespawnRequest
    {
        public int PlayerId { get; }
        public int SpawnIndex { get; }
        public CharacterID Id { get; }
    }
}