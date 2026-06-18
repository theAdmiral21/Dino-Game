
using System;
using Core.Equipment;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct ReloadResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(ReloadResult);
        public Type EquipmentActionType => ResultType;

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;


        private readonly ActionPhase _phase;

        public ReloadResult(bool approved)
        {
            _approved = approved;
            _phase = ActionPhase.Impulse;
        }
    }
}