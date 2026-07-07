using Core.Environment.Interactions;
using Primitives.Physics.Enums;
using UnityEngine;

namespace Unity.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class Ladder : MonoBehaviour, IClimbable
    {

        public ClimbObject ClimbSurface => _climbSurface;
        [SerializeField] private ClimbObject _climbSurface;

        public Vector2 Center => _center;
        private Vector2 _center;

        private void Awake()
        {
            Collider2D collider = GetComponent<Collider2D>();
            Bounds bounds = collider.bounds;
            _center = bounds.center;
        }

        public ClimbType GetClimbType(Vector2 climberPos)
        {
            // Determine where the climber is relative to the center of the ladder in the y direction
            float dir = climberPos.y - _center.y;

            // if the climber is at the top
            if (dir >= 0)
            {
                if (ClimbSurface == ClimbObject.Ladder)
                {
                    return ClimbType.LadderTop;
                }
                else
                {
                    return ClimbType.StairsTop;
                }
            }
            else
            {
                if (ClimbSurface == ClimbObject.Ladder)
                {
                    return ClimbType.LadderBottom;
                }
                else
                {
                    return ClimbType.StairsBottom;
                }
            }
        }
    }
}