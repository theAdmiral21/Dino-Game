using UnityEngine;
using Game.Core.Audio;
using Primitives.Audio;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using Core.Game.Audio;

namespace Application.Game.Audio.DataStructures
{
    public struct TrackRequest : ITrackRequest
    {
        public EntityKey Entity => EntityKey.Song;

        public ActionSoundKey ActionKey => ActionSoundKey.None;

        public AudioBehavior Behavior { get; private set; }

        public Vector2 VolumeRange { get; private set; }

        public Vector2 PitchRange { get; private set; }

        public SurfaceType Surface => SurfaceType.None;

        public TrackKey Track { get; private set; }

        public TrackRequest(TrackKey track, AudioBehavior behavior, float volume = 1)
        {
            Track = track;
            Behavior = behavior;
            VolumeRange = volume * Vector2.one;
            PitchRange = Vector2.one;
        }
    }
}
