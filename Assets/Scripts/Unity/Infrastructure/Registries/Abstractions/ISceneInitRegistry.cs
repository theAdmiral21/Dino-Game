using System.Collections.Generic;
using Game.Core.Execution;
using Infrastructure.Core.Registries;

namespace Infrastructure.Unity.Registries
{
    public interface ISceneInitRegistry
    {
        public IReadOnlyCollection<IInitializable<IGameContext>> Systems { get; }

    }
}