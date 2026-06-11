using System.Collections;
using Game.Core.Scenes;
using Primitives.Common.Scenes;

namespace Game.Scenes.Application
{
    public interface ISceneLoader : ISceneDefinitionProvider
    {
        // public ISceneContext ResolveScene(SceneId sceneId);
        public IEnumerator LoadSceneRoutine(ISceneDefinition sceneId);
    }
}