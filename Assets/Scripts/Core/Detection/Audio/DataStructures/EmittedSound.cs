using Primitives.Detection;
using UnityEngine;

namespace Core.Detection.Audio.DataStructures
{
    public struct EmittedSound
    {
        public Vector2 Origin; // uhh what else?
        public float Radius;
        public SoundType Type;
    }
}