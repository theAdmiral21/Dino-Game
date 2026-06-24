using UnityEngine;

namespace Physics.Unity.Physics
{
    /// <summary>
    /// Helper class that performs raycasts.
    /// </summary>
    public class RaycastConfig : MonoBehaviour
    {

        protected Vector2 _raySpacingX;
        protected Vector2 _raySpacingY;
        public int RaycastCountVertical { get; set; }
        public int RaycastCountHorizontal { get; set; }


        // public RaycastConfiguration ConfigRaycasts(Collider2D collider)
        // {
        //     UnityColliderBoundsProvider provider = new(collider);
        //     return new RaycastConfiguration(provider);

        // }

        // public void CalculateSpacing(Collider2D collider)
        // {
        //     Bounds bounds = collider.bounds;
        //     bounds.Expand(SKIN_WIDTH * -2);

        //     float xSpacing = bounds.size.x / (RaycastCountVertical - 1);
        //     float ySpacing = bounds.size.y / (RaycastCountHorizontal - 1);

        //     _raySpacingX = new Vector2(xSpacing, 0);
        //     _raySpacingY = new Vector2(0, ySpacing);
        // }

        // public void UpdateRaycastOrigins(Collider2D collider)
        // {
        //     Bounds bounds = collider.bounds;
        //     bounds.Expand(SKIN_WIDTH * -2);

        //     _rayCastOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
        //     _rayCastOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
        //     _rayCastOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        //     _rayCastOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        // }
    }
}