
using System;
using Core.Equipment;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct RaiseWeaponResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(RaiseWeaponResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;

        public Type EquipmentActionType => ResultType;

        private readonly ActionPhase _phase;

        public RaiseWeaponResult(bool approved, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}