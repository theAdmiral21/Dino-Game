using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface ISceneTag
    {
        public string LevelName { get; }

        public SceneId Id { get; }

    }
}