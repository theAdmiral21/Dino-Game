using Primitives.Common.Scenes;

namespace Game.Core.Scenes
{
    public interface IUnitySceneFlowManager
    {
        public bool IsTransitioning { get; }
        public void OnSceneChangeRequested(SceneId target);
    }
}