using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface ISceneDefinitionProvider
    {
        public ISceneDefinition ResolveScene(SceneId sceneId);
    }
}