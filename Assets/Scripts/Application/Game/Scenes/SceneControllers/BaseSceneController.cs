using Game.Core.Execution;
using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes
{
    public abstract class BaseSceneController : ISceneRunTimeController
    {
        protected IGameContext _gameContext;
        protected ISceneEvents _sceneEvents;
        public BaseSceneController(ISceneDefinition context, IGameContext gameContext)
        {
            _gameContext = gameContext;
            _sceneEvents = gameContext.SceneServices.SceneEvents;
            _sceneEvents.SceneChangeComplete += HandleSceneLoaded;
            _sceneEvents.SceneChangeStarted += HandleSceneUnloaded;
        }
        // Methods for triggering clean up steps
        private void HandleSceneLoaded(SceneId _) => OnSceneLoaded();
        private void HandleSceneUnloaded(SceneId _) => OnSceneUnloaded();


        public abstract void OnSceneLoaded();
        public abstract void OnSceneUnloaded();
        public abstract void Tick();
    }
}