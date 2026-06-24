using System.Collections.Generic;
using Game.Core.Execution;

namespace Infrastructure.Unity.Registries
{
    public interface ISceneInitRegistry
    {
        public IReadOnlyCollection<IInitializable<IGameContext>> Systems { get; }

    }
}