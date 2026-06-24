using System.Collections.Generic;
using Game.Core.Cameras;

namespace Infrastructure.Unity.Registries
{
    public interface ICameraRegistry
    {
        public IReadOnlyCollection<ICameraProvider> Cameras { get; }
    }
}