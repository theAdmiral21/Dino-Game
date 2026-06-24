using UnityEngine;

namespace Core.Light
{
    public interface ILightEmitter
    {
        public float Intensity { get; }
        public float Range { get; }
        public Vector2 Direction { get; }
    }
}