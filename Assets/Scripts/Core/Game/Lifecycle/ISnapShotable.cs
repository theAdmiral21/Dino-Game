namespace Core.Game.Lifecycle
{
    public interface ISnapShotable
    {
        public void TakeSnapShot();
        public void SerializeSnapShot();
    }
}