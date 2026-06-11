namespace Primitives.Easing.Enums
{
    public enum LoopEnum
    {
        // Teleport back to the start
        Restart,
        // Play the tween backwards
        PingPong,
        // Move the end position and go to it again
        Incremental,
        // Move to the next waypoint and then wait to be told to move again
        Index,
        // Stop whatever you're doing
        Stop,
    }
}