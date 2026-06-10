using Physics.Core.Abstractions;
using Primitives.Physics;
using UnityEngine;

namespace Gameplay.Common.Unity
{
    public class UnityColliderBoundsProvider : IBoundsProvider
    {
        private readonly Collider2D _collider;
        public UnityColliderBoundsProvider(Collider2D collider)
        {
            Debug.Log($"Constructing UnityColliderBoundsProvider - Frame {Time.frameCount}");
            _collider = collider;
        }
        public AABB GetBounds()
        {
            Bounds b = _collider.bounds;
            return new AABB
            {
                Center = b.center,
                Extents = b.extents,
                // Max = b.max,
                // Min = b.min,
                // Size = b.size,
            };
        }
    }
}