using Core.Light;
using UnityEngine;

namespace Unity.Detection.Emitters
{
    public class LightEmitter : MonoBehaviour, ILightEmitter
    {
        public float Intensity => throw new System.NotImplementedException();

        public float Range => throw new System.NotImplementedException();

        public Vector2 Direction => throw new System.NotImplementedException();
    }
}