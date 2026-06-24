using UnityEngine;

namespace Core.Light
{
    public interface ILightVolume : ILightEmitter
    {
        public Vector2 SourcePosition { get; }
        // public float Intensity { get; }
        // public float Range { get; }
    }
}