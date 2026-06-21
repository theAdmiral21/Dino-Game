using Core.Detection.Audio.DataStructures;

namespace Core.Detection.Audio
{
    public interface ISoundDetector
    {
        public float Sensitivity { get; }
        public void Detect(EmittedSound sound);
    }
}