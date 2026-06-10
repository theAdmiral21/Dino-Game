using Movement.Core.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Classifiers
{
    /// <summary>
    /// Rule type class used for determining what kind of jump the player requested. 
    /// </summary>
    public static class JumpClassifier
    {
        public static JumpType ClassifyRequestedJump(JumpContext jumpContext)
        {
            if (jumpContext.FloorMadeContact)
            {
                return JumpType.Ground;
            }
            return JumpType.Double;
        }
    }
}