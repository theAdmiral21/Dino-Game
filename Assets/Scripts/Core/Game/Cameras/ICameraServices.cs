namespace Game.Core.Cameras
{
    public interface ICameraService
    {
        public IActiveCameraChanger CameraChanger { get; }
    }
}