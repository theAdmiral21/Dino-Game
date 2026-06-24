using System.Collections.Generic;
using System.Text;
using AI.Core.PathFinding;
using UnityEngine;

namespace AI.Unity.PathFinding
{
    public class WeightedEscapePath : MonoBehaviour, IPathAwayFrom
    {
        [SerializeField] private float _lookAheadDistance = 5f;
        [SerializeField] private LayerMask _collisionLayer;
        [SerializeField] private bool _debug;
        private StringBuilder _debugSb = new();

        private readonly Vector2[] _directions = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down, Vector2.one, -Vector2.one, new Vector2(1, -1), new Vector2(-1, 1) };
        public IPathData PathAwayFrom(Vector2 currentPosition, Vector2 threatPosition)
        {
            // raycast in all 8 directions
            // Debug.Log($"Finding path away from: {threatPosition}");
            List<PathScore> paths = Search(currentPosition, threatPosition);
            // evaluate results

            return EvaluatePaths(paths);
        }

        private List<PathScore> Search(Vector2 currentPosition, Vector2 threatPosition)
        {
            if (_debug) _debugSb.Clear();
            List<PathScore> scores = new();
            // raycast in all 8 directions
            for (int i = 0; i < _directions.Length; i++)
            {
                Vector2 dir = _directions[i].normalized;
                RaycastHit2D hit = Physics2D.Raycast(currentPosition, dir, _lookAheadDistance, _collisionLayer);
                Vector2 awayBearing = (currentPosition - threatPosition).normalized;
                float awayScore = Vector2.Dot(dir, awayBearing);
                float clearanceScore;
                if (hit.collider == null)
                {
                    clearanceScore = 1;
                    if (_debug) DrawDebug(currentPosition, dir, _lookAheadDistance);
                }
                else
                {
                    clearanceScore = hit.fraction;
                    if (_debug) DrawDebug(currentPosition, dir, hit.distance);
                }
                PathScore path = new PathScore(i, awayScore, clearanceScore);

                if (_debug)
                {
                    _debugSb.AppendLine($"Dir: {_directions[i]}; awayScore: {awayScore}; clearance score: {clearanceScore}");
                }

                scores.Add(path);
            }
            Debug.Log(_debugSb);
            return scores;
        }

        private IPathData EvaluatePaths(List<PathScore> paths)
        {
            int bestIndex = 0;
            float bestScore = 0f;

            for (int i = 0; i < paths.Count; i++)
            {
                if (paths[i].TotalScore > bestScore)
                {
                    bestScore = paths[i].TotalScore;
                    bestIndex = i;
                }
            }

            return new WeightedPathResult(_directions[bestIndex]);
        }

        private void DrawDebug(Vector2 origin, Vector2 direction, float magnitude)
        {
            Debug.DrawRay(origin, magnitude * direction, Color.orange, 0.1f);
        }

        internal struct PathScore
        {
            public readonly int Index;
            public readonly float AwayScore;
            public readonly float ClearanceScore;
            public float TotalScore => AwayScore + ClearanceScore;

            public PathScore(int index, float awayScore, float clearanceScore)
            {
                Index = index;
                AwayScore = awayScore;
                ClearanceScore = clearanceScore;
            }
        }
    }
}