namespace Core.Game.Lifecycle
{
    public interface ISaveManager
    {
        public void SerializeEntities();
        public void SnapShotEntities();
        public void RevertEntities();
        public void ResetEntities();
        public void LoadEntitiesSnapShot();
    }
}