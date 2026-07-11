using Core.Movement.Abstractions;
using Core.WeaponRules;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Movement.Core.Abstractions;
using Movement.Unity.Abstractions;
using NPC.Core.Rules;
using Primitives.Rules;
using Unity.Common;
using UnityEngine;

namespace NPC.Unity.Providers
{
    public class RuleProvider : SelfRegister<IInitializable<IGameContext>>, IRuleStateProvider, IInitializable<IGameContext>
    {
        [SerializeField] private RuleSet _ruleSet;
        // [SerializeField] private SerializedInterface<IStatSheet> _statSheetMono;
        public IStatSheet StatSheet;

        public IRuleState RuleStateView { get; private set; }
        [SerializeField] private int _priority = 1;
        public int Priority => _priority;

        public void Initialize(IGameContext context)
        {
            // Get the stat sheet
            StatSheet = ProviderLookUp.Require<IStatProvider>(this).StatSheet;

            switch (_ruleSet)
            {
                case RuleSet.Throwable:
                    {
                        RuleStateView = new ThrowableRules();
                        break;
                    }
                case RuleSet.Raptor:
                    {
                        RuleStateView = new RaptorRules(StatSheet.StatCollection);
                        break;
                    }
            }
            Debug.Assert(StatSheet != null, $"Unable to find stat sheet");
            Debug.Assert(RuleStateView != null, $"Unable to construct new rule state");
        }

        public void PostInitialize(IGameContext context)
        {

        }
    }
}