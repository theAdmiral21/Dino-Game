
using System;
using Core.Equipment;

namespace Movement.Core.Movement.DataStructures
{
    public struct ToggleFlashLightRequest : IActionRequest, IEquipmentActionResult
    {
        public readonly Type RequestType => typeof(ToggleFlashLightRequest);
        public Type EquipmentActionType => typeof(ToggleFlashLightResult);
        public ToggleFlashLightRequest(bool yes = true)
        {
        }
    }
}