using UnityEngine;
using System.Collections.Generic;
using System;
using Movement.Core.Movement.DataStructures;
using Infrastructure.Core.Lifecycle.PhysicsEntities;
using Movement.Core.Abstractions;
using AI.Core.State.BehaviorContext;
using Movement.Core.Inputs;
using AI.Core.State;
using Enemy.Core.Detectors.Abstractions;
using AI.Core.Timers;
using Primitives.Detectors;
using Movement.Core.Enums;
using Physics.Core.PhysicsActors;
using NPC.Core.Effects;
using Game.Core.Health;
using System.Linq.Expressions;
using Movement.Core.Stats;
using Primitives.Stats.DataStructures;

namespace Enemy.Application.StateContexts
{
    public class EnemyContext : IMoveToContext,
                                ITickTimerContext,
                                IGameTimerContext,
                                IDetectPlayerContext,
                                ISearchAreaContext,
                                IDieContext
    {
        public Vector2 LastKnownLocation { get; private set; }

        public bool FoundPlayer { get; private set; }


        public Vector2 Destination { get; private set; }
        public Vector2 CurrentSpeed { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public ITimerContext Timer { get; private set; }

        public float Dt { get; set; }

        public IActionRequestSink RequestSink { get; private set; }
        public bool IsAlive => CurrentHealth > 0;
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

        public MovementType MoveType => MovementType.Run;

        private List<Vector2> _path = new List<Vector2>();
        private int _ndx = 0;
        private IAiInput _aiInput;
        private IDestructible _destructible;
        private IPlayerDetector _detector;
        private IActorEventBus _actorEventBus;
        private float _searchTime = 3f;
        private float _searchCounter;
        private IHealthComponent _npcHealth;
        private IStatCollection _stats;
        private float _runSlowDownRadius;
        private float _flySlowDownRadius;
        public EnemyContext(List<Vector2> wayPoints,
                            IAiInput aiInput,
                            IHealthComponent npcHealth,
                            IDestructible destructible,
                            IPlayerDetector detector,
                            IActorEventBus actorEventBus,
                            IStatCollection stats)
        {
            // Build a list of waypoints
            _path.AddRange(wayPoints);

            _npcHealth = npcHealth;

            // Setup object destruction
            _destructible = destructible;

            // Get control of the actor's inputs
            _aiInput = aiInput;

            _detector = detector;

            _actorEventBus = actorEventBus;

            _stats = stats;
            CalcSlowDown();

        }

        /// <summary>
        /// This method looks weird because of the way flying movement is calculated. As of now I have been lazy and just reused the speed value from run stats to determine the top flying speed.
        /// </summary>
        private void CalcSlowDown()
        {
            if (!_stats.TryGet<RunStats>(out var runStats)) Debug.LogError("StatCollection does not contain RunStats");

            _runSlowDownRadius = Mathf.Pow(runStats.RunSpeed.Value, 2) / (2 * runStats.BrakeAccel.Value);

            if (!_stats.TryGet<AerialStats>(out var flyStats)) Debug.LogError("StatCollection does not contain AerialStats");

            _flySlowDownRadius = Mathf.Pow(runStats.RunSpeed.Value, 2) / (2 * flyStats.AerialBrake.Value);

        }
        public Vector2 GetNextWayPoint()
        {
            _ndx = (_ndx + 1) % _path.Count;
            // Debug.Log($"Returning waypoint: {_path[_ndx]} of index: {_ndx}");
            return _path[_ndx];
        }

        public void Stop()
        {
            _aiInput.SetMove(Vector2.zero);
        }

        public void SetDestination(Vector2 dest)
        {
            Destination = dest;
        }

        public void MoveTo()
        {
            Vector2 moveVector = new();

            float bearingX;
            float slowDownRadius = MoveType == MovementType.Run ? _runSlowDownRadius : _flySlowDownRadius;

            // Add a fudge factor or else the robot will turn around when idling because it over shoots
            slowDownRadius *= 2;
            // Debug.Log($"slowDownRadius: {slowDownRadius}");

            bearingX = Destination.x - CurrentPosition.x;
            moveVector.x = Mathf.Sign(bearingX);// * Mathf.Clamp01(Mathf.Abs(bearingX) / slowDownRadius);

            float bearingY = Destination.y - CurrentPosition.y;
            moveVector.y = MathF.Sign(bearingY);

            // Debug.Log($"move vector: {moveVector}");
            _aiInput.SetMove(moveVector);
        }



        public IDetectionData DetectPlayer()
        {
            if (!_npcHealth.IsAlive) return null;
            return _detector.DetectPlayer();
        }

        public void SetFoundPlayer(bool val)
        {
            FoundPlayer = val;
        }
        public void SetLastKnowLocation(Vector2 location)
        {
            LastKnownLocation = location;
        }

        public void FaceLeft()
        {
            // Debug.Log($"Face left");
            _aiInput.FaceLeft(true);
        }

        public void FaceRight()
        {
            // Debug.Log($"Face right");
            _aiInput.FaceLeft(false);
        }

        public void SearchArea()
        {
            // _aiInput.SetMove(Vector2.zero);
            // Debug.Log($"Search Area is mainly for show right now..");
            _detector.DetectPlayer();
        }

        public void EmitAlertEvent()
        {
            _actorEventBus.Publish(new AlertResult());
        }

        public void EmitPassiveEvent()
        {
            _actorEventBus.Publish(new PassiveResult());
        }

        public bool CheckIsAlive()
        {
            return _npcHealth.IsAlive;
        }

    }
}