namespace Primitives.Detection
{
    public enum PlayerStatus
    {
        Aiming,        // weapon raised, looking around
        Reloading,    // can't shoot
        Healing,      // consuming item
        Interacting,  // solving puzzle, opening door
        Sprinting,    // moving fast, less aware
        Idle,          // standing still, could be watching
        Crawling,
        Walking,
        Unknown,
    }
}