using Primitives.Audio;
using UnityEngine;

namespace Core.Detection.Audio
{
    public interface ISoundEmitter
    {
        public Vector2 Origin { get; }
        public float MinRadius { get; }
        public SurfaceType GetSurface();
        public float GetSpeed();
        public void EmitSound(float soundRadius);
    }
}