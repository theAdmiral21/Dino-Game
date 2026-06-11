using Core.Movement.Abstractions;
using Movement.Core.Abstractions;
using Movement.Core.State.DataStructures;
using Movement.Core.Stats;
using Movement.Unity.Abstractions;
using Unity.Common.Unity;
using UnityEngine;

namespace PlayerController.Unity.Providers
{
    public class PlayerRuleStateProvider : MonoBehaviour, IRuleStateProvider
    {
        public IRuleState RuleStateView { get; private set; }
        [SerializeField] private SerializedInterface<IStatProvider> _statProviderMono;

        [Header("Debug Rule State")]
        [SerializeField] private PlayerRuleState _ruleState;

        private void Awake()
        {
            IStatCollection statCollection = _statProviderMono.Interface.StatSheet.StatCollection;
            _ruleState = new PlayerRuleState(statCollection);
            RuleStateView = _ruleState;
        }

    }
}