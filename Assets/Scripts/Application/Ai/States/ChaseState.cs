using UnityEngine;
using AI.Core.State.Abstractions;
using Movement.Core.Movement.DataStructures;
using AI.Core.State;
using Primitives.Detectors;

namespace AI.Application.States
{
    /// <summary>
    /// A simple enemy state for moving back and forth over a set distance.
    /// </summary>
    public class ChaseState<T> : IState<T> where T : IDetectPlayerContext, IMoveToContext
    {
        private bool _foundPlayer;
        private Vector2 _lastKnownLocation;
        public void Enter(T context)
        {
            Debug.Log($"🐬: MERMAID ANGRY.");
        }

        public void Update(T context)
        {
            Debug.Log($"🐬: Chasing player!");
            IDetectionData data = context.DetectPlayer();
            if (data != null)
            {
                if (data.ReadingQuality == DetectionReading.Current)
                {
                    _foundPlayer = true;
                    _lastKnownLocation = data.ObjectPos;
                    context.SetLastKnowLocation(_lastKnownLocation);
                    context.SetDestination(_lastKnownLocation);
                }
                else
                {
                    _foundPlayer = false;
                }
                context.SetFoundPlayer(_foundPlayer);
                context.MoveTo();
            }
        }

        public void Exit(T context)
        {
            Debug.Log($"🐬: Chase ended");
        }
    }
}