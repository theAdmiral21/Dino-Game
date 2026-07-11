using Game.Core.Effects;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Game.Core.Audio
{
    public interface IAudioBridge
    {
        public void PlaySound(EntityKey entity, ActionSoundKey actionKey);
        public void PlaySound(EntityKey entity, ActionSoundKey actionKey, SurfaceType surface);
        public void PlaySound(EntityKey entity, ActionSoundKey actionKey, SurfaceType surface, AudioBehavior behavior);
        public void PlaySound(EntityKey entity,
                                ActionSoundKey actionKey,
                                SurfaceType surface,
                                AudioBehavior behavior,
                                float volumeRange
                                );
        public void PlaySound(EntityKey entity,
                            ActionSoundKey actionKey,
                            SurfaceType surface,
                            AudioBehavior behavior,
                            float volumeRange,
                            float pitchRange
                            );
    }
}