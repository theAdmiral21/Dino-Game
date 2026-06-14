
using System;

namespace Movement.Core.Movement.DataStructures
{
    public struct RaiseWeaponRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(RaiseWeaponRequest);
        public readonly bool SetAiming;

        public RaiseWeaponRequest(bool setAiming)
        {
            SetAiming = setAiming;
        }
    }
}