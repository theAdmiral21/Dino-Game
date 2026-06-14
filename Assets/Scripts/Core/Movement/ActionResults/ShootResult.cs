
using System;
using Core.Equipment;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct ShootResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(ShootResult);
        public Type EquipmentActionType => ResultType;

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public ShootResult(bool approved, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}