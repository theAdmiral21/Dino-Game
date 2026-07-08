
using System;
using Core.Equipment;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Movement.DataStructures
{
    public struct IndexEquipmentResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(IndexEquipmentResult);
        public Type EquipmentActionType => typeof(IndexEquipmentResult);
        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public readonly int DeltaNdx;

        public IndexEquipmentResult(bool approved, int deltaNdx)
        {
            _approved = approved;
            _phase = ActionPhase.Override;
            DeltaNdx = deltaNdx;
        }
    }
}