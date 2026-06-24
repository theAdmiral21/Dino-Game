namespace Game.Core.Audio
{
    public interface ISongAudioRequest : IAudioRequest
    {
        public SongSoundKey SongKey { get; }

    }
}