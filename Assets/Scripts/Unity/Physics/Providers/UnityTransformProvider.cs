using Physics.Core.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace Gameplay.Common.Unity
{
    public class UnityTransformProvider : ITransformProvider
    {
        private readonly Transform _unityTransform;
        public UnityTransformProvider(Transform unityTransform)
        {
            _unityTransform = unityTransform;
        }
        public PhysicsTransform GetTransform()
        {
            return new PhysicsTransform
            {
                Position = _unityTransform.position,
                Scale = _unityTransform.localScale,
            };
        }
    }
}