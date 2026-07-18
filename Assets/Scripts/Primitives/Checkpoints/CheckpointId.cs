namespace Primitives.Checkpoints
{
    /// <summary>
    /// Checkpoint nomenclature: LevelName_ShortDescription, for example Jungle_TrexSighting
    /// </summary>
    public enum CheckpointId
    {
        // Level 1 jungle
        Jungle_Start,
        Jungle_DebugStart,

        // Power station
        PowerUp_Start,
        PowerUp_DebugStart,

        // Final level
        ControlCenter_Start,
        ControlCenter_DebugStart,

        // Dev level
        DevLevel_Start,
        DevLevel_DebugStart,
        DevLevel_Checkpoint1,

        None
    }
}