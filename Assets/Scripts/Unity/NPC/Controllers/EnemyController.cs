using UnityEngine;
using AI.Core.State.Abstractions;
using Gameplay.Common.Unity;
using System.Collections.Generic;
using Enemy.Application.StateContexts;
using AI.Application.States;
using AI.Application.State;
using Unity.Common.Unity;
using Physics.Unity.Actors;
using AI.Unity.StateMachine.BaseClasses;
using AI.Unity.StateMachine;
using System.Linq;
using Movement.Core.Inputs;
using Enemy.Core.Detectors.Abstractions;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Physics.Core.PhysicsActors;
using Movement.Core.Abstractions;
using Primitives.Damage;
using Movement.Core.Movement.DataStructures;
using NPC.Unity.Health;
using NPC.Core.Effects;
using System.Collections;
using Movement.Unity.Abstractions;

namespace Enemy.Unity
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PhysicsActor))]
    public class EnemyController : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [SerializeField] private SerializedInterface<IPhysicsActor> _actorMono;
        private IPhysicsActor _actor => _actorMono.Interface;
        [SerializeField] private SerializedInterface<IAiInput> _aiInputMono;
        private IAiInput _aiInput => _aiInputMono.Interface;
        [SerializeField] private NpcHealth _healthObject;
        [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;
        private IStatProvider _statProvider => _statProviderMono.Interface;

        [Header("Collision Zones")]
        [SerializeField] private TriggerVolume _stompZone;
        [SerializeField] private TriggerVolume _hurtZone;

        [Header("Pounce Bounce Values")]
        [SerializeField] private float _bounceTime;
        [SerializeField] private float _bounceHeight;


        [Header("Attack Values")]
        [SerializeField] private float _xDist;
        [SerializeField] private float _hitStunTime;
        [SerializeField] private float _knockBackApexTime;
        [SerializeField] private float _knockBackHeight;

        [Header("Patrol Values")]
        private Vector2 _dest;
        [SerializeField] private Transform _destObject;

        [Header("Player Detector")]
        [SerializeField] private SerializedInterface<IPlayerDetector> _detectorMono;
        private IPlayerDetector _detector => _detectorMono.Interface;

        private EnemyContext _context;

        [Header("States")]
        [SerializeField] private List<ScriptableState> _states;
        [Header("Transitions")]
        [SerializeField] private List<ScriptableTransition> _transitions;
        private Dictionary<ScriptableState, IState<EnemyContext>> _stateDict = new();
        private IStateMachine<EnemyContext> _stateMachine;


        [Header("Debug")]
        private string _currentState;

        private Vector2 _startPosition;
        public int Priority => 5;
        private void Awake()
        {
            base.Awake();

            _stateMachine = BuildStateMachine();
            _stateMachine.OnStateComplete += HandleStateComplete;
            _stateMachine.OnStateChanged += HandleStateChanged;


            // Subscribe to trigger zones
            _stompZone.OnVolumeEntered += OnStomped;
            _hurtZone.OnVolumeEntered += OnPlayerTouched;

        }

        private void HandleDeath()
        {
            // Clear colliders
            CleanUpEvents();
            Destroy(_stompZone);
            Destroy(_hurtZone);

            // Stop the scanner
            _detectorMono.Destroy();

            StartCoroutine(DeathRoutine());
        }

        private IEnumerator DeathRoutine()
        {
            Debug.Log($"Handling death");

            // Play death animation and a death sound
            _actor.Brain.ActorEventBus.Publish(new DeathResult());
            yield return new WaitForSeconds(3f);

            // Destroy actor
            _actor.MarkForDestruction();
        }

        private StateMachine<EnemyContext> BuildStateMachine()
        {
            foreach (ScriptableState baseState in _states)
            {
                // Cast from ScriptableState to ScriptableState<T> some how...
                ScriptableState<EnemyContext> state = (ScriptableState<EnemyContext>)baseState;
                // Debug.Log($"state: {state}; base state: {baseState}");
                _stateDict[baseState] = state.BuildRunTime();
                // Debug.Log($"keys in stateDict: {_stateDict.Keys.Count}");
            }

            var transitionList = new List<ITransition<EnemyContext>>();
            foreach (ScriptableTransition baseTransition in _transitions)
            {
                // Cast from ScriptableTransition to ScriptableTransition<T> some how...
                var transition = (ScriptableTransition<EnemyContext>)baseTransition;
                // Debug.Log($"keys in stateDict2: {_stateDict.Keys.Count}");
                transitionList.Add(transition.MapToRuntime(_stateDict));
            }

            return new StateMachine<EnemyContext>(_stateDict.Values.ToList(), transitionList);
        }

        private void OnDestroy()
        {
            CleanUpEvents();
            base.OnDestroy();
        }

        private void CleanUpEvents()
        {
            // Unsubscribe
            if (_stompZone != null) _stompZone.OnVolumeEntered -= OnStomped;
            if (_hurtZone != null) _hurtZone.OnVolumeEntered -= OnPlayerTouched;
            _healthObject.HealthComponent.OnDeath -= HandleDeath;
            _stateMachine.OnStateComplete -= HandleStateComplete;
        }

        private void OnStomped(IPhysicsActor entity)
        {

            var stunState = entity.Brain.GetCapability<IStunState>();
            if (stunState.IsStunned) return;

            // Give player a bounce
            IExternalForceReceiver receiver = entity.GetComponent<IExternalForceReceiver>();

            receiver.ReceiveImpulse(CalcPounceBounce());

            // Receive damage
            // _healthObject.ReceiveDamage(new DamageInfo(DamageType.Kill, Vector2.zero, 0, 0, 5));
        }

        private void OnPlayerTouched(IPhysicsActor player)
        {
            // Check if the actor is invincible
            var invincible = player.Brain.GetCapability<IInvincibleState>();
            // if it is, return


            if (invincible.IsInvincible) return;

            // Stun the player
            IStunnable stunReceiver = player.GetComponent<IStunnable>();
            stunReceiver.Stun(_hitStunTime);

            // Knock the player back
            IKnockBackable receiver = player.GetComponent<IKnockBackable>();

            Transform playerTransform = player.GetComponent<Transform>();
            var knockBackResult = CalcKnockBack(playerTransform.position);

            receiver.KnockBack(knockBackResult.Gravity, knockBackResult.Velocity);

            // Play an attack sound
            _actor.Brain.ActorEventBus.Publish(new AttackResult());
        }

        private ExternalImpulseRequest CalcKnockBack(Vector2 playerPos)
        {
            // Calculate the jump variables
            float gravity = -2 * _knockBackHeight / Mathf.Pow(_knockBackApexTime, 2);
            float yVel = Mathf.Abs(gravity) * _knockBackApexTime;
            float xDir = Mathf.Sign(playerPos.x - transform.position.x);
            float xVel = xDir * _xDist / (_knockBackApexTime / 2);
            var vel = transform.TransformDirection(new Vector2(xVel, yVel));
            // Debug.Log($"applying velocity: {vel} and gravity: {gravity}");
            return new ExternalImpulseRequest(vel, gravity);
        }

        private ExternalImpulseRequest CalcPounceBounce()
        {
            // Calculate the jump variables
            float gravity = -2 * _bounceHeight / Mathf.Pow(_bounceTime, 2);
            float yVel = Mathf.Abs(gravity) * _bounceTime;
            var vel = new Vector2(0, yVel);
            // Debug.Log($"Bounce vel: {vel}; bounce gravity: {gravity}");
            return new ExternalImpulseRequest(vel, gravity);
        }

        private void Update()
        {
            // Animate
            _context.Dt = Time.deltaTime;
            _context.CurrentPosition = transform.position;
            _context.CurrentSpeed = _actor.Brain.FrameData.CurrentState.Velocity;
            _stateMachine.Update(_context);

            _currentState = $"{_stateMachine.CurrentState}";

        }

        private void HandleStateComplete(IState<EnemyContext> state)
        {
            // StringBuilder dbString = new StringBuilder();
            // dbString.AppendLine($"Queued requests found: {_actor.ActionRequests.Count}  - Frame: {Time.frameCount}");
            // foreach (var queued in _actor.ActionRequests)
            // {
            //     dbString.AppendLine($"{queued.RequestType}");
            // }
            // Debug.Log(dbString);

            // We're changing states, clear the queue
            _actor.Brain.ActionRequests.Clear();
            // Debug.Log($"Completed state: {state}");

        }
        private void HandleStateChanged(IState<EnemyContext> state)
        {
            switch (state)
            {
                case MoveToState<EnemyContext> moveTo:
                    {
                        Vector2 dest = _context.GetNextWayPoint();
                        // Debug.Log($"got waypoint: {dest}");
                        _context.SetDestination(dest);
                        break;
                    }
            }
        }

        public void Initialize(IGameContext context)
        {
            // Can only get the actor event bus AFTER the brain is built. So this is for decoration.
        }

        public void PostInitialize(IGameContext context)
        {
            // Set up context
            _startPosition = transform.position;
            _dest = _destObject.position;
            List<Vector2> path = new List<Vector2> { _startPosition, _dest };

            _context = new EnemyContext(
                path,
                _aiInput,
                _healthObject.HealthComponent,
                _actor,
                _detector,
                _actor.Brain.ActorEventBus,
                _statProvider.StatSheet.StatCollection);

            // Subscribe to death
            _healthObject.HealthComponent.OnDeath += HandleDeath;

            Debug.Log($"Built enemy context");
            _stateMachine.Enter(_context);
        }
    }
}