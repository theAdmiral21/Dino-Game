using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Core.Game.Effects
{
    public interface IAudioEffect
    {
        public EntityKey Entity { get; }
        public ActionSoundKey ActionKey { get; }
        public SurfaceType Surface { get; }
    }
}