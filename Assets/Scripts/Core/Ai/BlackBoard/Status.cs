namespace Core.Ai.BlackBoard
{
    public enum Status
    {
        Searching, // trying to pick up the trail
        Tracking, // you are following a trail
        Stalking, // you have eyes on the target but you're keeping your distance
        Attacking, // you are actively engaging the target
        Retreating, // you are actively fleeing from an engagement
        Startled, // Something has happened, you don't know what, fight or flight time
        Flushing, // Charging the target with no intent to attack, trying to get them to run
        Flanking, // Moving to cut off escape routes anything else?
    }
}