namespace Editor
{
    public static class BehaviorTemplateProvider
    {
        public static string GetNodeClassTemplate(string behaviorName) =>
        $@"
using AI.Core.Behavior;
using Movement.Core.Movement.DataStructures;
using AI.Core.State.BehaviorContext;
using AI.Core.Timers;
using AI.Application.Timers;
using Core.Ai.State.BehaviorContext;
using Core.Ai.BlackBoard;
using Application.Utility;
using Movement.Core.Abstractions;
using Core.Ai.BlackBoard.DataStructures;

namespace AI.Application.BehaviorTreeNodes
{{
    public class {behaviorName}<T> : IBehaviorNode<T>
    {{
        public NodeResult Tick(T context)
        {{
            return NodeResult.Success;
        }}

        public void Reset(T context)
        {{
        
        }}
    }}
}}";

        public static string GetNodeSOTemplate(string behaviorName, string contextName, string contextShortHand) => $@"
using AI.Application.BehaviorTreeNodes;
using AI.Core.Behavior;
using NPC.Application.BehaviorContexts;
using UnityEngine;

namespace Unity.AI.BehaviorTree
{{
    [CreateAssetMenu(fileName = ""{behaviorName}"", menuName = ""AI/Enemy/Behaviors/{contextShortHand}/{contextShortHand} {behaviorName} SO"")]
    
    public class {contextShortHand}{behaviorName}SO : BehaviorNodeSO<{contextName}>
    {{
        public override IBehaviorNode<RaptorContext> BuildRunTime()
        {{
            return new {behaviorName}<{contextName}>();
        }}
    }}
}}";
    }
}