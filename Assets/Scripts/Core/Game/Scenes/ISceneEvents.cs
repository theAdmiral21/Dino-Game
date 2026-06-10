using System;
using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface ISceneEvents
    {
        public event Action SceneExit;
        public event Action<ISceneDefinition> SceneEntered;
        public event Action<SceneId> SceneChangeComplete;
        public event Action<SceneId> SceneChangeStarted;

        public void RaiseSceneExit();
        public void RaiseSceneEntered(ISceneDefinition context);
        public void RaiseChangeComplete(SceneId sceneId);
        public void RaiseSceneChangeStarted(SceneId sceneId);

    }
}