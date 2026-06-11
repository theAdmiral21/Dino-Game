using Game.Core.Scenes;
using Game.Unity.Scenes.DataStructures;
using Primitives.Common.Scenes;

namespace Game.Unity.Scenes
{
    public class SceneRegistry
    {
        private SceneLibrary _sceneLibrary;

        public SceneRegistry(SceneLibrary sceneLibrary)
        {
            _sceneLibrary = sceneLibrary;
        }

        public ISceneDefinition Resolve(SceneId sceneName)
        {
            // Debug.Log($"Resolving {sceneName}");
            return _sceneLibrary.GetScene(sceneName);
        }
    }
}