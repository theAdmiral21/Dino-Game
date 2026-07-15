namespace Core.Game.Lifecycle
{
    public interface ISnapShotable
    {
        public void TakeSnapShot();
        public string SerializeSnapShot();
        public void LoadSnapShot(string json);
    }
}