using System;
using System.Reflection;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using Core.Ai.State.BehaviorContext;
using Core.Detection;
using Core.Game.HealthSystem.Health;
using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Unity.Abstractions;
using Physics.Core.PhysicsActors;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Infrastructure.Providers
{
    public class RaptorDataProvider : SelfRegister<IInitializable<IGameContext>>,
                                        IInitializable<IGameContext>,
                                        IStatProvider,
                                        IHealthComponentProvider,
                                        IRuleStateProvider,
                                        IActorProvider,
                                        IActionRequestSinkProvider,
                                        IActorEventBusProvider,
                                        IPackDataProvider,
                                        IRaptorInputContext,
                                        IActorInputContext,
                                        IDetectorOrchestratorProvider,
                                        IRaptorControllerProvider
    {
        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        public IStatSheet StatSheet => _statSheetMono.Interface;

        [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        public IHealthComponent HealthComponent => _healthComponentMono.Interface.HealthComponent;

        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        public IRuleState RuleStateView => _ruleStateMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IPhysicsActor> _actorMono;
        public IPhysicsActor Actor => _actorMono.Interface;

        [SerializeField] private SerializedInterface<IActionRequestSink> _requestSinkMono;
        public IActionRequestSink RequestSink => _requestSinkMono.Interface;

        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        public IActorEventBus ActorEventBus => _actorEventBusMono.Interface.ActorEventBus;

        public PackData PackData => PackDataProvider.PackData;
        public IPackDataProvider PackDataProvider { get; private set; }
        public void SetPackDataProvider(IPackDataProvider packDataProvider) => PackDataProvider = packDataProvider;

        [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        public IActorInput ActorInput => _actorInputMono.Interface;
        [SerializeField] private SerializedInterface<IRaptorInput> _raptorInputMono;
        public IRaptorInput RaptorInput => _raptorInputMono.Interface;

        [SerializeField] private SerializedInterface<IDetectorBrainProvider> _detectorBrainMono;
        public IDetectorBrain DetectorBrain => _detectorBrainMono.Interface.DetectorBrain;

        [SerializeField] private SerializedInterface<IDetectorOrchestrator> _detectorOrchestratorMono;
        public IDetectorOrchestrator DetectorOrchestrator => _detectorOrchestratorMono.Interface;

        [SerializeField] private SerializedInterface<IRaptorController> _raptorControllerMono;
        public IRaptorController RaptorController => _raptorControllerMono.Interface;

        [SerializeField] private int _priority = 50;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            ValidateFields();
        }

        public void PostInitialize(IGameContext context)
        {
            // ValidateFields();
        }

        private void ValidateFields()
        {
            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (!field.FieldType.IsGenericType) continue;
                if (field.FieldType.GetGenericTypeDefinition() != typeof(SerializedInterface<>)) continue;

                var fieldValue = field.GetValue(this);
                var interfaceProp = field.FieldType.GetProperty("Interface");
                var resolved = interfaceProp.GetValue(fieldValue);

                if (resolved == null)
                {
                    Debug.LogError($"{name}: {GetType().Name}.{field.Name} is unassigned!");
                }

            }
        }
    }
}


