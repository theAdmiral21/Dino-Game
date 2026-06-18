
using System;
using UnityEngine;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Core.Equipment;

namespace Movement.Core.Movement.DataStructures
{
    public struct AimResult : IActionResult, IEquipmentActionResult
    {
        public Type ResultType => typeof(AimResult);
        public Type EquipmentActionType => ResultType;

        public bool Approved => _approved;
        private readonly bool _approved;

        public ActionPhase Phase => _phase;


        private readonly ActionPhase _phase;

        public readonly Vector2 MousePosition;

        public AimResult(bool approved, Vector2 mousePosition)
        {
            _approved = approved;
            _phase = ActionPhase.Override;
            MousePosition = mousePosition;
        }
    }
}