namespace Game.Core.Cameras
{
    public interface IActiveCameraChanger
    {
        public void AddCamera(ICameraHandle handle);
        public void RemoveCamera(ICameraHandle handle);
    }
}