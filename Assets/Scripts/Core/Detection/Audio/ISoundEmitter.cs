using UnityEngine;

namespace Core.Detection.Audio
{
    public interface ISoundEmitter
    {
        public Vector2 Origin { get; }
        public float MinRadius { get; }
        public void EmitSound();
    }
}