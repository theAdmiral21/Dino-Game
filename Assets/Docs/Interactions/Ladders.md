# Ladders

Ladders need to do the following:
- Allow climbing up or down
- Stopping in place (basically floating at a set height)
- Climbing from the bottom to the top
- Climbing from the top to the bottom

To interact with the ladder the player needs to press up when at the bottom of the ladder to climb up. If the player is at the top of a ladder they can press down to climb down. If the player is on a ladder they can choose to climb up or down.

For this to work I will need to implement climbing rules in my input manager, new capabilities, and new stats.

```c#
public enum ClimbType
{
    Ladder,
    Stairs,
}
public interface IClimbingState
{
    public bool IsClimbing {get;}
    public ClimbType ClimbingSurface {get;}
}

using System;

namespace Primitives.Stats.DataStructures
{
    [Serializable]
    public struct ClimbStats
    {
        public Type RuntimeType => typeof(ClimbStats);

        public Stat StairSpeed;
        public Stat StairAccel;
        public Stat StairBrake;

        public Stat LadderSpeed;
        public Stat LadderAccel;
        public Stat LadderBrake;
    }
}

```