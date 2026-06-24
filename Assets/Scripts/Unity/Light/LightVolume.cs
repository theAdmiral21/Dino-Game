using Core.Light;
using UnityEngine;

namespace Unity.Light
{
    public class LightVolume : MonoBehaviour, ILightVolume
    {
        public Vector2 SourcePosition => _sourceTransform.position;
        [SerializeField] private Transform _sourceTransform;

        public float Intensity => _intensity;
        [SerializeField] private float _intensity;

        public float Range => _lightRange;
        [SerializeField] private float _lightRange;

        public Vector2 Direction => throw new System.NotImplementedException();

    }
}