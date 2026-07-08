
using System;
using Core.Equipment;

namespace Movement.Core.Movement.DataStructures
{
    public struct IndexEquipmentRequest : IActionRequest, IEquipmentActionRequest
    {
        public readonly Type RequestType => typeof(IndexEquipmentRequest);

        public Type EquipmentActionType => typeof(IndexEquipmentRequest);

        public readonly int DeltaNdx;
        public IndexEquipmentRequest(int deltaNdx)
        {
            DeltaNdx = deltaNdx;
        }
    }
}