namespace Movement.Core.Enums
{
    /// <summary>
    /// This is used to assign order to different actions.
    /// 
    /// Continuous Actions:
    /// run, fall, wallslide, swim, etc
    /// 
    /// Impulse Actions:
    /// jump, wall jump, dash
    /// 
    /// Override:
    /// teleport, ledge snap, basically anything that must ignore kinematics
    /// 
    /// </summary>
    public enum ActionPhase
    {
        Continuous = 0,
        Impulse = 10,
        Override = 20,
    }
}