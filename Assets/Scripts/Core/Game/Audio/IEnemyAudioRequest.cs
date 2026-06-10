using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface IEnemyAudioRequest : IAudioRequest
    {
        public EnemyEntityKey EntityKey { get; }
        public ActionSoundKey ActionKey { get; }

    }
}