using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Game.Unity.Scenes.DataStructures;
using Game.Scenes.Application;
using Primitives.Common.Scenes;
using Game.Core.Scenes;
using Unity.Environment.Checkpoints.DataStructures;

namespace Game.Unity.Scenes
{
    public sealed class UnitySceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private SceneMapSO _sceneMap;

        public ISceneDefinition ResolveScene(SceneId sceneId)
        {
            ISceneDefinition sceneData = _sceneMap.GetSceneDefinition(sceneId);
            // Debug.Log($"Resolved {sceneId} to {sceneData.Tag.LevelName}");
            return sceneData;
        }
        public ISceneDefinition ResolveScene(string sceneName)
        {
            ISceneDefinition sceneData = _sceneMap.GetSceneDefinition(sceneName);
            // Debug.Log($"Resolved {sceneId} to {sceneData.Tag.LevelName}");
            return sceneData;
        }
        public IEnumerator LoadSceneRoutine(ISceneDefinition sceneData)
        {
            // Start async load
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneData.SceneName);
            asyncLoad.allowSceneActivation = false;

            // Wait until load is ready
            while (asyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            // Optional delay for polish
            yield return new WaitForSeconds(0.1f);

            // Activate the scene
            asyncLoad.allowSceneActivation = true;

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

        }
    }
}