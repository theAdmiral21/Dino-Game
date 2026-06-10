using System.Collections.Generic;

namespace Game.Core.Cameras
{
    public interface ICameraManager
    {
        public IReadOnlyCollection<ICameraProvider> CameraRegistry { get; }

        public ICameraHandle GetActiveCamera();
    }
}