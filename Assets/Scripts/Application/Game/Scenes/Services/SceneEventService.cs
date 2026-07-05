using System;
using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes.Services
{
    public sealed class SceneEventService : ISceneEvents
    {
        public event Action<ISceneDefinition> SceneEntered;

        /// <summary>
        /// This is the event that is raised when a scene turns over control to the player
        /// </summary>
        public event Action<SceneId> SceneChangeComplete;
        public event Action<SceneId> SceneChangeStarted;
        public event Action SceneExit;

        public void RaiseSceneEntered(ISceneDefinition context)
        {
            SceneEntered?.Invoke(context);
        }

        public void RaiseChangeComplete(SceneId sceneId)
        {
            SceneChangeComplete?.Invoke(sceneId);
        }

        public void RaiseSceneChangeStarted(SceneId sceneId)
        {
            SceneChangeStarted?.Invoke(sceneId);
        }

        public void RaiseSceneExit()
        {
            SceneExit?.Invoke();
        }
    }
}