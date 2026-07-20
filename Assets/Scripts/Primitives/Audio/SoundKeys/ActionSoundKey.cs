namespace Primitives.Audio.SoundKeys
{
    public enum ActionSoundKey
    {
        Bark,
        Howl,
        // Do I need walk, run, jump, and land? That's handled by the surface set.
        Walk,
        Run,
        Jump,
        DoubleJump,
        Land,
        Attack,
        Hurt,
        Die,
        PickUp,
        Talk,
        WarCry,
        Roar,
        AreaEnter,
        Spawn,
        Explode,
        CompleteLevel,
        StartZoomies,
        EndZoomies,
        ZoomiesTwinkle,
        MermaidAppears,
        Pump,
        Lunge,
        Interact,
        Crouch,
        Dodge,

        // Doors
        Open,
        Close,
        Unlock,
        Lock,

        // Menu actions
        Start,
        Select,
        Submit,
        Back,
        Cancel,

        // Results
        Success,
        Failure,

        // Fall back enum
        None,
    }
}