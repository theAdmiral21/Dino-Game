using System.Reflection;
using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Unity.Abstractions;
using Physics.Core.PhysicsActors;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Infrastructure.Providers
{
    public class RockDataProvider : SelfRegister<IInitializable<IGameContext>>,
                                        IInitializable<IGameContext>,
                                        IStatProvider,
                                        IRuleStateProvider,
                                        IActorProvider,
                                        IActorEventBusProvider,
                                        IActorInputContext
    {
        [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        public IActorInput ActorInput => _actorInputMono.Interface;

        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        public IStatSheet StatSheet => _statSheetMono.Interface;

        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        public IRuleState RuleStateView => _ruleStateMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IPhysicsActor> _actorMono;
        public IPhysicsActor Actor => _actorMono.Interface;

        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        public IActorEventBus ActorEventBus => _actorEventBusMono.Interface.ActorEventBus;

        [SerializeField] private int _priority = 4;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            ValidateFields();
        }

        public void PostInitialize(IGameContext context)
        {

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