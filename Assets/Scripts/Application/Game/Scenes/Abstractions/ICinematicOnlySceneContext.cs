using System.Collections.Generic;
using Game.Core.Cinematics.Enums;
using Game.Core.Scenes;

namespace Game.Application.Scenes.Abstractions
{
    public interface ICinematicOnlySceneContext : ISceneDefinition
    {
        public ISceneTag NextScene { get; }
        public List<CinematicId> Cinematics { get; }
    }
}