
using System;
using Core.Equipment;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct SwitchEquipmentResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(SwitchEquipmentResult);

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;

        public Type EquipmentActionType => typeof(SwitchEquipmentResult);

        private readonly ActionPhase _phase;

        public readonly int EquipmentNdx;

        public SwitchEquipmentResult(bool approved, int equipmentNdx)
        {
            _approved = approved;
            _phase = ActionPhase.Override;
            EquipmentNdx = equipmentNdx;
        }
    }
}