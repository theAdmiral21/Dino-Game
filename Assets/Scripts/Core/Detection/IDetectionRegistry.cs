using System.Collections.Generic;
using Core.Detection.Audio;

namespace Core.Detection
{
    public interface IDetectionRegistry
    {
        public IReadOnlyCollection<ISoundDetector> SoundDetectors { get; }
        public IReadOnlyCollection<ISoundEmitter> SoundEmitters { get; }

        // public IReadOnlyCollection<IVisualDetector> VisualDetectors { get; }
        // public IReadOnlyCollection<IVisualEmitter> VisualEmitters { get; }

        // public IReadOnlyCollection<IScentDetector> ScentDetectors { get; }
        // public IReadOnlyCollection<IScentEmitter> ScentEmitters { get; }
    }
}