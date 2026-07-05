using UnityEngine;
using AI.Core.Behavior;
using AI.Core.State;
using AI.Core.State.BehaviorContext;
using AI.Core.PathFinding;
using Movement.Core.Abstractions;
using Core.Ai.State.BehaviorContext;
using Core.Ai.BlackBoard;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;

namespace AI.Application.BehaviorTreeNodes
{
    public class RunAway<T> : IBehaviorNode<T> where T : IDetectPlayerContext, IPositionContext, IPathFindContext, IAInputContext, IStatusContext
    {
        public string DisplayName => "RunAway";
        public float LastTickTime { get; private set; }
        public NodeResult LastResult { get; private set; }
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();

        public float MinSafeDistance => _minSafeDistance;
        private float _minSafeDistance;
        public RunAway(float minSafeDistance)
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

            var res = Flee(context);
            // flee
            Debug.Log($"Node result for run away result: {res}");
            return res;
        }

        private NodeResult Flee(T context)
        {
            context.SetStatus(Status.Retreating);
            float dist = Vector2.Distance(context.CurrentPosition, context.LastKnownLocation);
            if (dist > _minSafeDistance)
            {
                // Stop moving
                context.AiInput.SetMove(Vector2.zero);
                context.AiInput.SetJumpPressed(false);
                Debug.Log($"Escaped!");
                return NodeResult.Success;
            }
            else
            {
                // Calculate escape vectors
                IPathData path = PlotEscape(context);
                // Choose one at random
                Debug.Log($"Escaping along vector: {path.Bearing}");
                // Send the input
                context.AiInput.SetMove(path.Bearing);

                // Add a jump?
                if (path.Bearing.y > 0)
                {
                    context.AiInput.SetJumpPressed(true);
                }
                else
                {
                    context.AiInput.SetJumpPressed(false);
                }

                // Execute
                return NodeResult.Running;
            }
        }

        private IPathData PlotEscape(T context)
        {
            // Calculate the escape vector
            IPathData pathData = context.FindPath(context.CurrentPosition);
            return pathData;
        }

        public void Reset(T context)
        {
            Debug.Log($"Reset run away node");
            context.AiInput.SetMove(Vector2.zero);
            context.AiInput.SetJumpPressed(false);
        }
    }
}