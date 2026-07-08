
using System;
using Core.Equipment;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct ToggleFlashLightResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(ToggleFlashLightResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;

        public Type EquipmentActionType => typeof(ToggleFlashLightResult);

        private readonly ActionPhase _phase;

        public ToggleFlashLightResult(bool approved)
        {
            _approved = approved;
            _phase = ActionPhase.Override;
        }
    }
}