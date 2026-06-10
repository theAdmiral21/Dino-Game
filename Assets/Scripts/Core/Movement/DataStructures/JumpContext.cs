using UnityEngine;

namespace Movement.Core.DataStructures
{
    public struct JumpContext
    {
        public readonly Vector2 FloorHitNormal;
        public readonly bool FloorMadeContact;
        public readonly Vector2 WallHitNormal;
        public readonly bool WallMadeContact;

        public JumpContext(
            Vector2 floorHitNormal = new Vector2(),
            bool floorMadeContact = false,
            Vector2 wallHitNormal = new Vector2(),
            bool wallMadeContact = false
         )
        {
            FloorHitNormal = floorHitNormal;
            FloorMadeContact = floorMadeContact;
            WallHitNormal = wallHitNormal;
            WallMadeContact = wallMadeContact;
        }
    }
}