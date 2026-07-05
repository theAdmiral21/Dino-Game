using Codice.Client.BaseCommands;
using Core.Movement.Abstractions;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Core.State.DataStructures;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using Unity.Common;
using Unity.Common.Unity;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace PlayerController.Unity.Providers
{
    public class PlayerRuleStateProvider : SelfRegister<IInitializable<IGameContext>>, IRuleStateProvider, IInitializable<IGameContext>
    {
        public IRuleState RuleStateView { get; private set; }
        [SerializeField] private int _priority;
        public int Priority => _priority;

        // [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;
        private IStatProvider _statProvider;

        [Header("Debug Rule State")]
        [SerializeField] private PlayerRuleState _ruleState;


        public void Initialize(IGameContext context)
        {
            _statProvider = ProviderLookUp.Require<IStatProvider>(this);

            IStatCollection statCollection = _statProvider.StatSheet.StatCollection;
            _ruleState = new PlayerRuleState(statCollection);
            RuleStateView = _ruleState;
            Debug.Log($"RuleStateView: {RuleStateView}");

            Debug.Assert(_statProvider != null, $"Failed to find stat provider!");
            Debug.Assert(RuleStateView != null, $"Failed to initialize rule state!");

        }

        public void PostInitialize(IGameContext context)
        {
        }
    }
}