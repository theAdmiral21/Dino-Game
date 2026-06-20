
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
        private readonly ActionPhase _phase;

        public bool IsRaising => _isRaising;
        private readonly bool _isRaising;

        public Type EquipmentActionType => ResultType;


        public RaiseWeaponResult(bool approved, bool isRaising, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
            _isRaising = isRaising;
        }
    }
}