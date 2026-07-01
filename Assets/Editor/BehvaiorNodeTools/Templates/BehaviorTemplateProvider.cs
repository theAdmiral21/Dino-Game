namespace Editor
{
    public static class BehaviorTemplateProvider
    {
        public static string GetNodeClassTemplate(string behaviorName) =>
        $@"
using AI.Core.Behavior;
using UnityEngine;
using System.Collections.Generic;
using Core.Ai.Behavior.Visualization;
using System;

namespace AI.Application.BehaviorTreeNodes
{{
    public class {behaviorName}<T> : IBehaviorNode<T>
    {{
        public string DisplayName => ""{behaviorName}"";
        public NodeResult LastResult {{ get; private set; }}
        public float LastTickTime {{ get; private set; }}
        public IReadOnlyList<IInspectableNode> Children => Array.Empty<IInspectableNode>();

        public NodeResult Tick(T context)
        {{
            LastResult = TickInternal(context);
            LastTickTime = Time.time;
            return LastResult;
        }}

        private NodeResult TickInternal(T context)
        {{
            return NodeResult.Success;
        }}

        public void Reset(T context)
        {{
            Debug.Log($""Resetting {behaviorName}"");
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