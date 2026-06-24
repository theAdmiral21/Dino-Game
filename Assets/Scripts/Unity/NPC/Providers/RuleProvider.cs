using Core.Movement.Abstractions;
using Core.WeaponRules;
using Enemy.Core.Rules;
using Movement.Core.Abstractions;
using Movement.Unity.Abstractions;
using Primitives.Rules;
using Unity.Common.Unity;
using UnityEngine;

namespace NPC.Unity.Providers
{
    public class RuleProvider : MonoBehaviour, IRuleStateProvider, IStatProvider
    {
        [SerializeField] private RuleSet _ruleSet;
        [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        public IStatSheet StatSheet => _statSheetMono.Interface;

        public IRuleState RuleStateView { get; private set; }

        private void Awake()
        {
            switch (_ruleSet)
            {
                case RuleSet.Throwable:
                    {
                        RuleStateView = new ThrowableRules();
                        break;
                    }
                case RuleSet.Raptor:
                    {
                        RuleStateView = new EnemyRules();
                        break;
                    }

            }
        }
    }
}