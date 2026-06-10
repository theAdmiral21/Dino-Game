

using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface ICurrentSceneProvider
    {
        public SceneId CurrentScene { get; }

    }
}