using System;
using AI.Core.PathFinding;
using AI.Core.State;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using Core.Ai.Behavior;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using Core.Ai.State.BehaviorContext;
using Core.Detection.DataStructures;
using Core.Movement.Inputs;
using Enemy.Core.Detectors.Abstractions;
using Movement.Core.Abstractions;
using Movement.Core.Enums;
using Movement.Core.Movement.DataStructures;
using Movement.Core.Stats;
using Primitives.Detection;
using Primitives.Detectors;
using UnityEngine;

namespace NPC.Application.BehaviorContexts
{
    public class RaptorContext : IBehaviorContext,
                                 IMoveToContext,
                                 IPositionContext,
                                 IDetectPlayerContext,
                                 IPathFindContext,
                                 ITickTimerContext,
                                 IGameTimerContext,
                                 IInputContext,
                                 IRaptorInputContext,
                                 IPerceptionContext,
                                 IStatusContext,
                                 IAlertContext,
                                 IPackDataContext,
                                 IDetectorContext,
                                 ISearchAreaContext,
                                 IStatSheet
    {
        public MovementType MoveType => MovementType.Run;

        public Vector2 Destination { get; private set; }

        public Vector2 CurrentSpeed { get; set; }

        public Vector2 CurrentPosition { get; set; }

        public bool FoundPlayer { get; private set; }

        public Vector2 LastKnownLocation { get; private set; }

        public float Dt { get; set; }

        // public IAiInput AiInput => _aiInput;

        public PerceptionState Perception { get; private set; }

        public ITimerContext Timer => throw new NotImplementedException();

        public Status CurrentStatus { get; private set; }

        public PackData PackData => _packDataProvider.PackData;

        public AlertLevel Alertness { get; private set; }

        public DetectorStats DetectionStats { get; private set; }

        public IStatCollection StatCollection { get; private set; }

        public IRaptorInput RaptorInput { get; set; }

        public IAiInput AiInput => RaptorInput;

        private IPackDataProvider _packDataProvider;

        private IPlayerDetector _detector;
        private IPathAwayFrom _pathFinder;
        public RaptorContext(IRaptorInput raptorInput, IPackDataProvider packDataProvider, DetectorStats detectionStats, IStatCollection stats)
        {
            RaptorInput = raptorInput;
            _packDataProvider = packDataProvider;
            DetectionStats = detectionStats;
            StatCollection = stats;
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
            Debug.Log($"Calc'd move input: {moveVector}");
            RaptorInput.SetMove(moveVector);
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
            RaptorInput.SetMove(Vector2.zero);
        }

        public IPathData FindPath(Vector2 relevantPosition)
        {
            return _pathFinder.PathAwayFrom(CurrentPosition, LastKnownLocation);
        }

        public void UpdatePerception(PerceptionState state)
        {
            // Debug.Log($"Updating perception state");
            Perception = state;
        }

        public void SetStatus(Status status)
        {
            CurrentStatus = status;
        }

        public void SetAlertLevel(AlertLevel alertness)
        {
            Alertness = alertness;
        }

        public void FaceLeft()
        {
            // Debug.Log($"Face left");
            RaptorInput.FaceLeft(true);
        }

        public void FaceRight()
        {
            // Debug.Log($"Face right");
            RaptorInput.FaceLeft(false);
        }

        public void SearchArea()
        {
            throw new NotImplementedException();
        }
    }
}