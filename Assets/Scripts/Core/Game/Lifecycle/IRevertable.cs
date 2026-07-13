namespace Core.Game.Lifecycle
{
    public interface IRevertable : ISnapShotable
    {
        public void Revert();
    }
}