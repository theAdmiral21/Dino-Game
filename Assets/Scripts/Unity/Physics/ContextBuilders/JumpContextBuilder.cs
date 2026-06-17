using System;
using Core.Movement.Abstractions;
using Core.Physics.Collisions;
using Movement.Core.Abstractions;
using Movement.Core.Classifiers;
using Movement.Core.DataStructures;
using Movement.Core.Rules;
using Movement.Core.Stats;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsQueries;
using Physics.Unity.Physics;
using Primitives.Stats.DataStructures;
using UnityEngine;

namespace Physics.Unity.ContextBuilders
{
    public class JumpContextBuilder : IJumpContextBuilder
    {
        private RaycastProbe _probe;
        private IStatCollection _stats;

        // Specific stat objects
        JumpStats _jumpStats;
        WallStats _wallJumpStats;
        private LayerMask _hazardLayer;
        public JumpContextBuilder(IStatCollection stats)
        {
            // I know this looks funny, but all hazards exist on the player layer
            _hazardLayer = LayerMask.GetMask("Player");

            _probe = new(LayerMask.GetMask("Hazard"));
            Debug.Assert(_probe != null, $"Failed to assign raycast probe.");

            _stats = stats;
            Debug.Assert(_stats != null, $"Failed to assign stat collection.");


            // Cache your stats to reduce look ups
            CacheStatValues();
        }

        private void CacheStatValues()
        {
            if (_stats.TryGet<JumpStats>(out var jumpVals))
            {
                _jumpStats = jumpVals;
            }
            else
            {
                Debug.LogError($"Failed to cache JumpStats");
            }

            if (_stats.TryGet<WallStats>(out var wallVals))
            {
                _wallJumpStats = wallVals;
            }
            else
            {
                Debug.LogError($"Failed to cache WallStats");
            }
        }

        public JumpContext BuildJumpContext(RaycastConfiguration raycastConfig, Vector2 velocity, float gravity, float facing)
        {
            float jumpBufferDuration = _jumpStats.JumpBufferTime.Value;
            float wallJumpBufferDuration = _wallJumpStats.WallJumpBufferTime.Value;
            // Perform the raycast
            var floorHit = GetFloorHits(raycastConfig, jumpBufferDuration, velocity, gravity);
            var wallHit = GetWallHits(raycastConfig, wallJumpBufferDuration, velocity, gravity, facing);

            // return the context
            return new JumpContext(
                floorHit.normal,
                floorHit.collider != null,
                wallHit.normal,
                wallHit.collider != null
                );
        }

        private RaycastHit2D GetWallHits(RaycastConfiguration raycastConfig, float bufferDuration, Vector2 velocity, float gravity, float facing)
        {
            // How far the player will move horizontally
            float xDist = velocity.x * bufferDuration + raycastConfig.SkinWidth;

            // How far the player will move vertically
            float yDist = velocity.y * bufferDuration + (0.5f * gravity * bufferDuration * bufferDuration) + raycastConfig.SkinWidth;
            // Vector2 distVector = new Vector2(xDist, yDist);

            // Don't let falling extend your wall jump distance
            Vector2 distVector = new Vector2(xDist, 0f);

            // Determine the distance
            float castDist = distVector.magnitude;

            // Determine the direction
            Vector2 castDir;
            if (velocity.x == 0)
            {
                castDir = facing * Vector2.right;
            }
            else
            {
                castDir = new Vector2(xDist, yDist).normalized;
            }

            // Get the face
            ColliderFace face = GetLeftRightFace(facing);

            Debug.Log($"Cast Dist: {castDist}; Cast Dir: {castDir}; Face: {face}");

            // Perform the cast
            RaycastHit2D hits = _probe.FaceCast(face, castDist, castDir, raycastConfig, Color.yellow, true);

            // Perform a second "safety" cast that eats the first cast if the player is about to hit a hazard
            RaycastHit2D safetyHits = _probe.FaceCast(face, castDist, castDir, raycastConfig, _hazardLayer, Color.red, false);

            bool safety = IsSafe(hits, safetyHits);
            // If the player won't land safely, nullify the result to force a double jump
            if (!safety)
            {
                hits = new RaycastHit2D();
            }


            return hits;
        }

        private ColliderFace GetLeftRightFace(float facing)
        {
            if (facing > 0f) return ColliderFace.Right;

            if (facing < 0f) return ColliderFace.Left;

            throw new ArgumentOutOfRangeException();
        }

        private RaycastHit2D GetFloorHits(RaycastConfiguration raycastConfig, float bufferDuration, Vector2 velocity, float gravity)
        {
            // How far the player will move horizontally
            float xDist = velocity.x * bufferDuration; // NOTE Do I want to include x velocity in the distance calculation? 

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
            RaycastHit2D hits = _probe.FaceCast(ColliderFace.Bottom, castDist, castDir, raycastConfig, Color.yellow, true);

            // Perform a second "safety" cast that eats the first cast if the player is about to hit a hazard
            RaycastHit2D safetyHits = _probe.FaceCast(ColliderFace.Bottom, castDist, castDir, raycastConfig, _hazardLayer, Color.red, true);

            bool safety = IsSafe(hits, safetyHits);
            // If the player won't land safely, nullify the result to force a double jump
            if (!safety)
            {
                hits = new RaycastHit2D();
            }


            return hits;
        }

        private bool IsSafe(RaycastHit2D hits, RaycastHit2D safetyHits)
        {
            // If we didn't find anything, there is nothing to worry about
            if (safetyHits.collider == null || hits.collider == null) return true;

            // If the floor is closer than the danger we're good
            if (hits.fraction < safetyHits.fraction)
            {
                return true;
            }

            return false;
        }
    }
}