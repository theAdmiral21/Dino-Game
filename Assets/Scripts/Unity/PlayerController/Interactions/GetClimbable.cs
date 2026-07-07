using Core.Environment.Interactions;
using Core.Physics.PhysicsQueries;
using Physics.Core.DataStructures;
using Primitives.Physics.DataStructures;
using UnityEngine;

namespace Unity.PlayerController.Interactions
{
    public class GetClimbable : MonoBehaviour, IGetClimbable
    {
        [SerializeField] private LayerMask _climbLayer;

        [Header("Debug")]
        [SerializeField] private bool _debug;
        public IClimbable FindClimbable(RaycastConfiguration rayConfig)
        {
            // perform an overlap box check using your collider
            Vector2 center = rayConfig.Bounds.Center;
            Vector2 size = rayConfig.Bounds.Size;

            Collider2D overlap = Physics2D.OverlapBox(center, size, 0f, _climbLayer);
            Debug.Log($"Got collider: {overlap}");
            if (_debug) ColliderDraw(rayConfig.Origins);
            if (overlap != null)
            {
                if (overlap.TryGetComponent<IClimbable>(out var climbable))
                {
                    return climbable;
                }
            }
            return null;
        }

        private void ColliderDraw(RaycastOrigins raycastOrigins)
        {
            Debug.DrawLine(raycastOrigins.BottomLeft, raycastOrigins.TopLeft, Color.blue, .1f);
            Debug.DrawLine(raycastOrigins.BottomLeft, raycastOrigins.BottomRight, Color.blue, .1f);
            Debug.DrawLine(raycastOrigins.TopRight, raycastOrigins.TopLeft, Color.blue, .1f);
            Debug.DrawLine(raycastOrigins.BottomRight, raycastOrigins.TopRight, Color.blue, .1f);
        }
    }
}