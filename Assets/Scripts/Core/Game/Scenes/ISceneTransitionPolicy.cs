namespace Game.Core.Scenes
{
    public interface ISceneTransitionPolicy
    {
        // public bool EvaluateRequest(SceneId targetScene, SceneId currentScene, GameState currentGameState);
        public bool EvaluateRequest(ISceneChangeRequest sceneChangeRequest);
    }
}