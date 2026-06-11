namespace Game.Core.Cinematics.Enums
{
    public enum CinematicState
    {
        /// <summary>
        /// Is actively playing.
        /// </summary>
        Playing,
        /// <summary>
        /// Was playing and is now paused.
        /// </summary>
        Paused,
        /// <summary>
        /// Is not playing and is available to do something.
        /// </summary>
        Waiting,
        /// <summary>
        /// Empty state for initialization.
        /// </summary>
        None,
    }
}