using System;
using AI.Core.PathFinding;
using AI.Core.State;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using Core.Ai.State.BehaviorContext;
using Core.Detection.DataStructures;
using Core.Movement.Inputs;
using Enemy.Core.Detectors.Abstractions;
using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Primitives.Detectors;
using UnityEngine;

namespace NPC.Application.BehaviorContexts
{
    public class RaptorContext : IMoveToContext,
                               IPositionContext,
                               IDetectPlayerContext,
                               IPathFindContext,
                               ITickTimerContext,
                               IGameTimerContext,
                               IInputContext,
                               IPerceptionContext
    {
        public MovementType MoveType => MovementType.Run;

        public Vector2 Destination { get; private set; }

        public Vector2 CurrentSpeed { get; set; }

        public Vector2 CurrentPosition { get; set; }

        public bool FoundPlayer { get; private set; }

        public Vector2 LastKnownLocation { get; private set; }

        public float Dt { get; set; }

        public IAiInput AiInput => _aiInput;

        public PerceptionState Perception { get; private set; }

        public ITimerContext Timer => throw new NotImplementedException();

        private IAiInput _aiInput;
        private IPlayerDetector _detector;
        private IPathAwayFrom _pathFinder;
        private float _runSlowDownRadius;
        private float _flySlowDownRadius;
        public RaptorContext(IAiInput aiInput)

        {
            _aiInput = aiInput;

        }

        public IDetectionData DetectPlayer()
        {
            return _detector.DetectPlayer();
        }

        public void EmitAlertEvent()
        {
            throw new System.NotImplementedException();
        }

        public void EmitPassiveEvent()
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetNextWayPoint()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo()
        {
            Vector2 moveVector = new();

            float bearingX;

            bearingX = Destination.x - CurrentPosition.x;
            moveVector.x = Mathf.Sign(bearingX);

            float bearingY = Destination.y - CurrentPosition.y;
            moveVector.y = MathF.Sign(bearingY);
            // Debug.Log($"Calc'd move input: {moveVector}");
            _aiInput.SetMove(moveVector);
        }

        public void SetDestination(Vector2 dest)
        {
            Destination = dest;
            // Debug.Log($"Set destination to: {Destination}");
        }

        public void SetFoundPlayer(bool val)
        {
            FoundPlayer = val;
        }

        public void SetLastKnowLocation(Vector2 location)
        {
            LastKnownLocation = location;
        }

        public void Stop()
        {
            _aiInput.SetMove(Vector2.zero);
        }

        public IPathData FindPath(Vector2 relevantPosition)
        {
            return _pathFinder.PathAwayFrom(CurrentPosition, LastKnownLocation);
        }

        public void UpdatePerception(PerceptionState state)
        {
            Perception = state;
        }
    }
}