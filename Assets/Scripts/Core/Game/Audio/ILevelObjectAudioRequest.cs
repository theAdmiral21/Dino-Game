using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface ILevelObjectAudioRequest : IAudioRequest
    {
        public LevelObjectEntityKey EntityKey { get; }
        public ActionSoundKey ActionKey { get; }

    }
}