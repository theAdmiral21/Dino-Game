using Game.Core.Cameras;

namespace Game.Application.Cameras
{
    public class CameraServices : ICameraService
    {
        public IActiveCameraChanger CameraChanger { get; private set; }

        public CameraServices(IActiveCameraChanger cameraAdder)
        {
            CameraChanger = cameraAdder;
        }
    }
}