using Game.Core.Scenes;
using Game.Core.State.Services;

namespace Game.Application.Scenes.Services
{
    public class SceneServices : ISceneServices
    {
        public ICurrentSceneProvider CurrentSceneService { get; private set; }

        public IChangeSceneService SceneChangeService { get; private set; }

        public ISceneEvents SceneEvents { get; private set; }

        public ISyncSceneService SyncSceneService { get; private set; }

        public ISceneDefinitionProvider SceneDefinitionProvider { get; private set; }

        public SceneServices(
            SceneStateManager sceneStatusManager,
            ISceneEvents sceneEvents,
            IGameStateProvider gameStateProvider,
            ISceneDefinitionProvider sceneContextProvider
            )
        {
            CurrentSceneService = new CurrentSceneProviderService(sceneStatusManager);

            SceneChangeService = new SceneChangeService(
                sceneStatusManager,
                CurrentSceneService,
                gameStateProvider);

            SceneEvents = sceneEvents;

            SyncSceneService = new SyncSceneService(sceneStatusManager);

            SceneDefinitionProvider = sceneContextProvider;
        }
    }
}