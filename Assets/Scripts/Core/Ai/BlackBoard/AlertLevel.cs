namespace Core.Ai.BlackBoard
{
    public enum AlertLevel
    {
        Unaware, // minding your own business
        Curious, // You heard or smelled something
        Interested, // You heard/smelled something player like
        Engaged, // You might have a fix on the player
        Engrossed, // You see the player, you are obsessed, a psychiatrist would say its unhealthy
    }
}