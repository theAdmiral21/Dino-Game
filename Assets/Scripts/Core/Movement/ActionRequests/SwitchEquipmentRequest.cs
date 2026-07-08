
using System;
using Core.Equipment;

namespace Movement.Core.Movement.DataStructures
{
    public struct SwitchEquipmentRequest : IActionRequest, IEquipmentActionResult
    {
        public readonly Type RequestType => typeof(SwitchEquipmentRequest);

        public Type EquipmentActionType => typeof(SwitchEquipmentRequest);

        public readonly int EquipmentNdx;
        public SwitchEquipmentRequest(int equipmentNdx)
        {
            EquipmentNdx = equipmentNdx;
        }
    }
}