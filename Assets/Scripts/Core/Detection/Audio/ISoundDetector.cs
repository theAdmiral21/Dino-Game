using System;
using Core.Detection.Audio.DataStructures;

namespace Core.Detection.Audio
{
    public interface ISoundDetector
    {
        public event Action<AudioData> AudioEvent;
        public float Sensitivity { get; }
        public void Listen(EmittedSound sound);
    }
}