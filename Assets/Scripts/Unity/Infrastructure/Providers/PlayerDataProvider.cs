using System;
using System.Reflection;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using Core.Ai.State.BehaviorContext;
using Core.Detection;
using Core.Equipment;
using Core.Game.HealthSystem.Damage;
using Core.Game.HealthSystem.Health;
using Core.Movement.Abstractions;
using Core.Movement.Inputs;
using Game.Core.Execution;
using Game.Core.Health;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Unity.Abstractions;
using Physics.Core.PhysicsActors;
using PlayerController.Core.Info;
using Primitives.Damage;
using Primitives.Players;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Infrastructure.Providers
{
    public class PlayerDataProvider : SelfRegister<IInitializable<IGameContext>>,
                                        IInitializable<IGameContext>,
                                        IStatProvider,
                                        IHealthComponentProvider,
                                        IHealProvider,
                                        IDamageProvider,
                                        IRuleStateProvider,
                                        IActorProvider,
                                        IActionRequestSinkProvider,
                                        IActorEventBusProvider,
                                        IActorInputContext,
                                        IEquipmentManagerProvider,
                                        IPlayerInfoProvider
    {
        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        public IStatSheet StatSheet => _statSheetMono.Interface;

        [SerializeField] private SerializedInterface<IHealthComponentProvider> _healthComponentMono;
        public IHealthComponent HealthComponent => _healthComponentMono.Interface.HealthComponent;

        [SerializeField] private SerializedInterface<IHealProvider> _healProviderMono;
        public IHealable Healable => _healProviderMono.Interface.Healable;

        [SerializeField] private SerializedInterface<IDamageProvider> _damageProviderMono;
        public IDamageable Damageable => _damageProviderMono.Interface.Damageable;

        [SerializeField] private SerializedInterface<IRuleStateProvider> _ruleStateMono;
        public IRuleState RuleStateView => _ruleStateMono.Interface.RuleStateView;

        [SerializeField] private SerializedInterface<IPhysicsActor> _actorMono;
        public IPhysicsActor Actor => _actorMono.Interface;

        [SerializeField] private SerializedInterface<IActionRequestSink> _requestSinkMono;
        public IActionRequestSink RequestSink => _requestSinkMono.Interface;
        [SerializeField] private SerializedInterface<IActorEventBusProvider> _actorEventBusMono;
        public IActorEventBus ActorEventBus => _actorEventBusMono.Interface.ActorEventBus;

        [SerializeField] private SerializedInterface<IActorInput> _actorInputMono;
        public IActorInput ActorInput => _actorInputMono.Interface;


        [SerializeField] private SerializedInterface<IEquipmentManagerProvider> _equipmentManagerMono;
        public IEquipmentManager EquipmentManager => _equipmentManagerMono.Interface.EquipmentManager;

        [SerializeField] private SerializedInterface<IPlayerInfoProvider> _playerInfoMono;
        public IPlayerInfo PlayerInfo => _playerInfoMono.Interface.PlayerInfo;



        [SerializeField] private int _priority = 50;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            // throw new NotImplementedException();
            ValidateFields();
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(PlayerInfo != null, $"Player info is still null in the data provider..");
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


