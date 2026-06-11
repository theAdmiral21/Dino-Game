using System;
using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes.Abstractions
{
    public interface ISceneChangeCoordinator
    {
        public event Action<ISceneDefinition> EnteredNewSceneEvent;
        public void StartSceneChange(SceneId sceneId);
        public void FinishTransition();
    }


}