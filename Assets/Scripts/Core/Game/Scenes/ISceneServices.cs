namespace Game.Core.Scenes
{
    public interface ISceneServices
    {
        // Gets the current scene id
        public ICurrentSceneProvider CurrentSceneService { get; }
        // Requests a scene change
        public IChangeSceneService SceneChangeService { get; }
        // Scene event services
        public ISceneEvents SceneEvents { get; }
        // Method for syncing the scene state manager to the correct scene
        public ISyncSceneService SyncSceneService { get; }
        public ISceneDefinitionProvider SceneDefinitionProvider { get; }
    }
}