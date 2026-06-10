namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Interface that contracts movement with an entity.
    /// </summary>
    public interface IMovementController
    {
        public void Stop();
        public void MoveTo();
    };
}