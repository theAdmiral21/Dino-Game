using Game.Core.Audio;
using Game.Unity.Audio.DataStructures;
using Primitives.Audio.EntityKeys;

namespace Game.Unity.Audio.Abstractions
{
    public interface IEntitySoundSet : ISoundSet
    {
        public EntityKey Entity { get; }
    }
}