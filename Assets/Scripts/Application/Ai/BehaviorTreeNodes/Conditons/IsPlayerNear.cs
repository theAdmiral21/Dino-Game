using UnityEngine;
using AI.Core.Behavior;
using AI.Core.State;
using AI.Core.State.BehaviorContext;
using Primitives.Detectors;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;

namespace AI.Application.BehaviorTreeNodes
{
    public class IsPlayerNear<T> : IBehaviorNode<T> where T : IDetectPlayerContext, IPositionContext
    {

        public string DisplayName => "IsPlayerNear";
        public float LastTickTime { get; private set; }

        public NodeResult LastResult { get; private set; }
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();

        public float MinSafeDistance => _minSafeDistance;
        private float _minSafeDistance;
        private IDetectionData _trackingData;

        /*
        Success = The player is within the minimum safe distance
        Failure = The player is NOT within the minimum safe distance
        */
        public IsPlayerNear(float minSafeDistance)
        {
            _minSafeDistance = minSafeDistance;
        }
        public NodeResult Tick(T context)
        {
            LastResult = TickInternal(context);
            return LastResult;
        }
        private NodeResult TickInternal(T context)
        {
            LastTickTime = Time.time;

            // Returns Success or Failure
            var res = SearchForPlayer(context);
            Debug.Log($"Node result for player near: {res}");
            return res;
        }

        private NodeResult SearchForPlayer(T context)
        {
            // do the scan
            var data = context.DetectPlayer();
            // interpret the result and return it
            return Interpret(context, data);
        }

        private NodeResult Interpret(T context, IDetectionData data)
        {
            if (data == null) return NodeResult.Failure;

            if (data.ReadingQuality == DetectionReading.Current)
            {
                context.SetLastKnowLocation(data.ObjectPos);
                context.SetFoundPlayer(true);
                Debug.Log($"Found the player!");
                return NodeResult.Success;
            }
            else
            {
                context.SetFoundPlayer(false);
                return NodeResult.Failure;
            }
        }

        public void Reset(T context)
        {

        }
    }
}