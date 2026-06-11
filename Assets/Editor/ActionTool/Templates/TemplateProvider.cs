namespace Editor
{
    public static class TemplateProvider
    {
        public static string GetRequestTemplate(string actionName)
        => $@"
using System;

namespace Movement.Core.Movement.DataStructures 
{{
    public struct {actionName}Request : IActionRequest
    {{
        public readonly Type RequestType => typeof({actionName}Request);

        public {actionName}Request()
        {{
        }}
    }}
}}";

        public static string GetResultTemplate(string actionName)
=> $@"
using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures 
{{
    public struct {actionName}Result : IActionResult
    {{
        public Type ResultType => typeof({actionName}Result);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public {actionName}Result(bool approved,ActionPhase phase)
        {{
            _approved = approved;
            _phase = phase;
        }}
    }}
}}";

        public static string GetRuleTemplate(string actionName)
=> $@"
using Movement.Core.Movement.DataStructures;
using Movement.Core.Inputs;
using Primitives.Physics;

namespace Movement.Core.Rules
{{
    public static class {actionName}Rules
    {{
        public static {actionName}Result Try{actionName}({actionName}Request request, PhysicsContext facts, IActorInput inputValues, object ruleState)
        {{

        }}

        private static {actionName}Result Approved()
        {{

        }}

        private static {actionName}Result Denied()
        {{

        }}
        
}}
}}";

        public static string GetDispatchTemplate(string actionName)
=> $@"
using Movement.Application.Abstractions;
using Movement.Core.Rules;
using Movement.Core.Inputs;
using Movement.Core.Movement;
using Movement.Core.Movement.DataStructures;
using Primitives.GameState;
using Primitives.Physics;

namespace Movement.Application.Dispatchers
{{
    public sealed class {actionName}Dispatcher : DispatchRequestBase<{actionName}Request, {actionName}Result>
    {{
        protected override {actionName}Result Dispatch(in {actionName}Request request, in PhysicsContext facts, in GameState gameState, in IActorInput inputs, object ruleState)
        {{
            return {actionName}Rules.Try{actionName}(request, facts, inputs, ruleState);
        }}
    }}
}}";
    }
}