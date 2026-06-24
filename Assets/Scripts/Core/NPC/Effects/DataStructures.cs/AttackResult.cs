using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace NPC.Core.Effects
{
    public struct AttackResult : IActionResult
    {
        public Type ResultType => typeof(AttackResult);

        public bool Approved => true;

        // Leave the phase as is. It will be a warning because this is an effect action
        public ActionPhase Phase => throw new NotImplementedException();
    }
}