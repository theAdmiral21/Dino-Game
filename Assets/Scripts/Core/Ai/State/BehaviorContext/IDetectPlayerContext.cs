using Primitives.Detectors;
using UnityEngine;

namespace AI.Core.State
{
    public interface IDetectPlayerContext
    {
        public bool FoundPlayer { get; }
        public Vector2 LastKnownLocation { get; }
        public IDetectionData DetectPlayer();
        public void SetFoundPlayer(bool val);
        public void SetLastKnowLocation(Vector2 location);
        public void EmitAlertEvent();
        public void EmitPassiveEvent();
    }
}