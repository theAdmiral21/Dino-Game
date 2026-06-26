using Primitives.Detection;
using UnityEngine;

namespace Core.Detection.Audio.DataStructures
{
    public struct AudioData
    {
        public float DetectionTime;
        public float SoundIntensity;
        public SoundType Type; // I don't know if I actually want this or not. It could be interesting or it could be a lot of work for nothing
        public Vector2 SoundDirection;
    }
}