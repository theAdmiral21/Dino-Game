using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Application.Scenes
{
    public class SceneContextProvider : ISceneDefinitionProvider
    {
        private ISceneDefinitionProvider _contextProvider;
        public SceneContextProvider(ISceneDefinitionProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public ISceneDefinition ResolveScene(SceneId sceneId)
        {
            return _contextProvider.ResolveScene(sceneId);
        }

        public ISceneDefinition ResolveScene(string sceneName)
        {
            return _contextProvider.ResolveScene(sceneName);
        }
    }
}