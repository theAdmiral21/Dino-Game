using Physics.Core.Abstractions;
using Primitives.Physics;
using Primitives.Physics.DataStructures;
using UnityEngine;

namespace Physics.Core.DataStructures
{
    public class RaycastConfiguration
    {
        public readonly float SkinWidth;
        public RaycastOrigins Origins => _origins;
        private RaycastOrigins _origins;
        public Vector2 RaySpacingX { get; private set; }
        public Vector2 RaySpacingY { get; private set; }
        public AABB Bounds => _boundsProvider.GetBounds();
        public int RaycastCountVertical { get; set; }
        public int RaycastCountHorizontal { get; set; }
        private IBoundsProvider _boundsProvider;
        public int CollisionLayer;
        public int PhysicalLayer;
        public RaycastConfiguration(IBoundsProvider boundsProvider, int collisionLayer, int physicalLayer)
        {
            Debug.Log($"Constructing new raycast configuration with: {boundsProvider} - frame {Time.frameCount}");
            _boundsProvider = boundsProvider;
            CollisionLayer = collisionLayer;
            PhysicalLayer = physicalLayer;
            SkinWidth = 0.04f;
            _origins = new RaycastOrigins(_boundsProvider.GetBounds());
            RaySpacingX = Vector2.zero;
            RaySpacingY = Vector2.zero;
            RaycastCountVertical = 5;
            RaycastCountHorizontal = 5;
            CalculateSpacing();
        }


        public void UpdateRaycastOrigins()
        {
            Bounds.Expand(SkinWidth * -2);

            _origins.TopLeft = new Vector2(Bounds.Min.x, Bounds.Max.y);
            _origins.TopRight = new Vector2(Bounds.Max.x, Bounds.Max.y);
            _origins.BottomLeft = new Vector2(Bounds.Min.x, Bounds.Min.y);
            _origins.BottomRight = new Vector2(Bounds.Max.x, Bounds.Min.y);
        }

        private void CalculateSpacing()
        {
            Bounds.Expand(SkinWidth * -2);

            float xSpacing = Bounds.Size.x / (RaycastCountVertical - 1);
            float ySpacing = Bounds.Size.y / (RaycastCountHorizontal - 1);

            RaySpacingX = new Vector2(xSpacing, 0);
            RaySpacingY = new Vector2(0, ySpacing);
        }
    }
}