using System;
using System.Collections.Generic;
using AI.Core.State;
using AI.Core.State.BehaviorContext;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using UnityEngine;

namespace Enemy.Application.StateContexts
{
    public class MermaidContext : IMoveToContext,
                                  ITickTimerContext,
                                  IPositionContext,
                                  IConversationContext
    {
        public float Dt { get; set; }

        public bool IsTalking { get; private set; }
        public bool ConversationStarted { get; private set; }
        public Vector2 CurrentSpeed { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public Vector2 Destination { get; private set; }

        public string Conversation => throw new NotImplementedException();

        public MovementType MoveType => throw new NotImplementedException();

        // List for setting hidden and revealed destinations
        private List<Vector2> _path = new List<Vector2>();
        private int _ndx = 0;

        public MermaidContext(List<Vector2> wayPoints)
        {
            // Build a list of waypoints
            _path.AddRange(wayPoints);
        }

        public void GoToNextWayPoint()
        {
            _ndx = (_ndx + 1) % _path.Count;
            Destination = _path[_ndx];
        }

        public void ContinueConversation()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo()
        {
            Vector2 moveVector = new();

            float bearingX = Destination.x - CurrentPosition.x;
            moveVector.x = MathF.Sign(bearingX);

            float bearingY = Destination.y - CurrentPosition.y;
            moveVector.y = MathF.Sign(bearingY);
        }

        public void Stop()
        {
            return;
        }

        public void SetDestination(Vector2 dest)
        {
            throw new NotImplementedException();
        }

        public Vector2 GetNextWayPoint()
        {
            throw new NotImplementedException();
        }
    }
}