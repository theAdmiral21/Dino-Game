using System.Collections.Generic;
using Game.Core.Cameras;
using Infrastructure.Core.Registries;

namespace Infrastructure.Unity.Registries
{
    public interface ICameraRegistry
    {
        public IReadOnlyCollection<ICameraProvider> Cameras { get; }
    }
}