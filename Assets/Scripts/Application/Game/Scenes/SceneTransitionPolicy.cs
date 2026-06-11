using Game.Core.Scenes;
using Primitives.Common.Scenes;
using Primitives.GameState;

namespace Game.Systems.Scenes.Application
{
    /// <summary>
    /// Class for evaluating a scene transition request. This class answers if it is possible to change from one scene to another.
    /// </summary>
    public class SceneTransitionPolicy : ISceneTransitionPolicy
    {
        public bool EvaluateRequest(ISceneChangeRequest sceneChangeRequest)
        {
            if (!NewSceneIsNotCurrentScene(sceneChangeRequest.RequestedScene, sceneChangeRequest.CurrentScene)) return false;

            if (!NotAlreadyTransitioning(sceneChangeRequest.CurrentState)) return false;

            // If you made it to the end you're good.
            return true;
        }

        private bool NewSceneIsNotCurrentScene(SceneId sceneId, SceneId currentScene)
        {
            return sceneId != currentScene;
        }

        private bool NotAlreadyTransitioning(GameState currentGameState)
        {
            return currentGameState != GameState.Transitioning;
        }
    }
}