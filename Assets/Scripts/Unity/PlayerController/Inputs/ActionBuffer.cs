using Movement.Core.Movement.DataStructures;
using Primitives.Physics;
using Physics.Core.DataStructures;
using Physics.Unity.Physics;
using UnityEngine;
using Physics.Core.Buffers;

namespace PlayerController.Unity.Inputs
{
    public class ActionBuffer : MonoBehaviour, IActionBuffer
    {
        [SerializeField] private LayerMask _layerMask;
        private RaycastProbe _raycastProbe;
        private void Awake()
        {
            _raycastProbe = new RaycastProbe(_layerMask);
        }
        public JumpRequest BufferJumpRequest(RaycastConfiguration raycastConfig, float bufferDuration, Vector2 velocity, float gravity)
        {
            // How far the player will move horizontally
            float xDist = velocity.x * bufferDuration;

            // How far the player will move vertically
            float yDist = velocity.y * bufferDuration + (0.5f * gravity * bufferDuration * bufferDuration) + raycastConfig.SkinWidth;
            Vector2 distVector = new Vector2(xDist, yDist);

            // Determine the distance
            float castDist = distVector.magnitude;

            // Determine the direction
            Vector2 castDir;
            if (velocity.y == 0)
            {
                castDir = Vector2.down;
            }
            else
            {
                castDir = distVector.normalized;
            }

            // Debug.Log($"Cast Dist: {castDist}; Cast Dir: {castDir}");
            // Perform the cast
            RaycastHit2D hits = _raycastProbe.FaceCast(ColliderFace.Bottom, castDist, castDir, raycastConfig, Color.yellow, true);
            // Debug.Log($"Draw buffered jump");

            // Interpret results
            if (hits.collider != null)
            {
                // Debug.Log($"Buffer got collider: {hits.collider.name}");
                float dot = Vector2.Dot(hits.normal, Vector2.up);

                if (dot > 0.7f)
                {
                    Debug.Log($"Returning buffered ground jump");
                    return new JumpRequest(true, JumpType.Ground);
                }
            }
            Debug.Log($"Returning double jump");
            return new JumpRequest(true, JumpType.Double);
        }
    }
}