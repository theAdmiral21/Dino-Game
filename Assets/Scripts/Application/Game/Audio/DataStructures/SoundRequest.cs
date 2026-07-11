using UnityEngine;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;

namespace Application.Game.Audio.DataStructures
{
    public struct SoundRequest : IAudioRequest
    {
        public EntityKey Entity { get; private set; }
        public ActionSoundKey ActionKey { get; private set; }
        public AudioBehavior Behavior { get; private set; }
        public SurfaceType Surface { get; private set; }
        public Vector2 VolumeRange { get; private set; }
        public Vector2 PitchRange { get; private set; }

        public SoundRequest(EntityKey entity,
                            ActionSoundKey actionKey,
                            SurfaceType surface = SurfaceType.None,
                            AudioBehavior behavior = AudioBehavior.SingleShot)
        {
            Entity = entity;
            ActionKey = actionKey;
            Surface = surface;
            Behavior = behavior;
            VolumeRange = Vector2.one;
            PitchRange = Vector2.one;
        }

        public SoundRequest(EntityKey entity,
                    ActionSoundKey actionKey,
                    SurfaceType surface,
                    AudioBehavior behavior,
                    Vector2 volumeRange)
        {
            Entity = entity;
            ActionKey = actionKey;
            Surface = surface;
            Behavior = behavior;
            VolumeRange = volumeRange;
            PitchRange = Vector2.one;
        }

        public SoundRequest(EntityKey entity,
            ActionSoundKey actionKey,
            SurfaceType surface,
            AudioBehavior behavior,
            Vector2 volumeRange,
            Vector2 pitchRange)
        {
            Entity = entity;
            ActionKey = actionKey;
            Surface = surface;
            Behavior = behavior;
            VolumeRange = volumeRange;
            PitchRange = pitchRange;
        }
    }
}