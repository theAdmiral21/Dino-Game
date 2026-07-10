
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using AI.Application.Timers;
using Movement.Core.Abstractions;
using Core.Ai.State.BehaviorContext;
using Core.Ai.BlackBoard;

namespace AI.Application.BehaviorTreeNodes
{
    public class Idle<T> : IBehaviorNode<T> where T : ITickTimerContext, IGameTimerContext, IAInputContext, IStatusContext
    {
        public string DisplayName => "Idle";
        public NodeResult LastResult { get; private set; }
        public float LastTickTime { get; private set; }
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();

        private float _idleTime;
        public ITimerContext Timer { get; private set; }

        public Idle(float idleTime = 5f)
        {
            _idleTime = idleTime;
            Timer = new TimerContext(_idleTime);
        }

        public NodeResult Tick(T context)
        {
            LastResult = TickInternal(context);
            context.SetStatus(Status.Waiting);
            LastTickTime = Time.time;
            return LastResult;
        }

        private NodeResult TickInternal(T context)
        {
            if (!Timer.TimerComplete && !Timer.IsActive)
            {
                Debug.Log($"Starting idle timer");
                // Restart the timer
                Timer.StartTimer();
                context.AiInput.SetMove(Vector2.zero);
            }

            // Only move while the timer is running
            if (Timer.IsActive)
            {
                context.AiInput.SetMove(Vector2.zero);

                // Tick the timer 
                Timer.TickTimer(context.Dt);
                return NodeResult.Running;
            }
            context.AiInput.SetMove(Vector2.zero);
            return NodeResult.Success;
        }

        public void Reset(T context)
        {
            Timer.ResetTimer();
            // Debug.Log($"Resetting Idle");
        }
    }
}