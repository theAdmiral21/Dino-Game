using PlayerController.Unity.Animations;
using PlayerController.Unity.Effects;
using UnityEngine;
using Movement.Core.Inputs.DataStructures;
using Infrastructure.Unity.Registries;
using Game.Core.Execution;
using Game.Core.State.Services;
using Unity.Common.Unity;
using Movement.Core.Abstractions;
using Core.Movement.Abstractions;
using Movement.Core.Inputs;
using Movement.Core.Stats;
using Primitives.Stats.DataStructures;
using Physics.Core.Abstractions;
using Primitives.Physics;
using Physics.Core.PhysicsActors;

namespace PlayerController.Unity.Inputs
{
    /// <summary>
    /// Orchestrator for player controls. This class recieves input from input providers and converts them into action requests which are evaluated in PlayerController.Core and then returned as action results. The action results are then sent to the effect orchestrator for playing sound effects, the simulation driver for moving the player, and the animator bridge to animate the player.
    /// </summary>
    public class PlayerActionOrchestrator : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        // [SerializeField] private PlayerInputReader _inputReader;
        [SerializeField] private PlayerEffectOrchestrator _effectOrchestrator;
        [SerializeField] private AnimatorEffectBridge _animatorBridge;
        [SerializeField] private SerializedInterface<IActorInput> _actorInput;
        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateViewMono;
        private IRuleState _ruleStateView => _ruleStateViewMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IPhysicsActor> _physicsActorMono;
        private PhysicsContext _physicsContext => _physicsActorMono.Interface.Brain.CurrentContext;

        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        private IStatCollection _stats => _statSheetMono.Interface.StatCollection;
        public int Priority => 2;

        private InputState _inputState;
        private IGameStateProvider _gameState;
        private IChangeGameStateService _changeGameState;


        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            base.Awake();
            _inputState = new InputState();
        }

        public void Initialize(IGameContext context)
        {
            // Debug.Log($"Game state services: {context.GameStateServices.GameState}");
            _gameState = context.GameStateServices.GameState;
            // Debug.Log($"Game state: {_gameState}");

            _changeGameState = context.GameStateServices.ChangeGameState;
        }

        public void PostInitialize(IGameContext context)
        {
            var runStats = _stats.Get<RunStats>();
            var sprintStats = _stats.Get<SprintStats>();
            _effectOrchestrator.SetRunValues(runStats.RunSpeed.Value, sprintStats.SprintSpeed.Value);

        }

        private void Update()
        {
            // Update player state
            _effectOrchestrator.SetPhysicsContext(_physicsContext);
            // Update Rule state
            _effectOrchestrator.SetRuleState(_ruleStateView);

            _effectOrchestrator.EvaluateStateEffects(_ruleStateView);

            // Animate the state

            _animatorBridge.SyncAnimation(_actorInput.Interface, _physicsContext, _ruleStateView);

        }
    }
}