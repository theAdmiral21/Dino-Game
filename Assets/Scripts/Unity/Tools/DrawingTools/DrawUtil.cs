using UnityEngine;

namespace Unity.Tools.DrawingTools
{
    public static class DrawUtil
    {
        public static void DrawDebugCircle(Vector2 center, float radius, Color color, int segments = 32)
        {
            // Debug.Log($"Drawing circle at {center} with radius {radius}");
            float angleStep = 360f / segments;
            Vector2 prevPoint = center + new Vector2(radius, 0);

            for (int i = 1; i <= segments; i++)
            {
                float angle = angleStep * i * Mathf.Deg2Rad;
                Vector2 nextPoint = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
                Debug.DrawLine(prevPoint, nextPoint, color, .2f);
                prevPoint = nextPoint;
            }
        }
    }
}