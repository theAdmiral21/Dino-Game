using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Game.Unity.Scenes.DataStructures;
using Game.Scenes.Application;
using Primitives.Common.Scenes;
using Game.Core.Scenes;

namespace Game.Unity.Scenes
{
    public sealed class UnitySceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private SceneLibrary _sceneLibrary;
        private SceneRegistry _sceneRegistry;
        private void Awake()
        {
            _sceneRegistry = new SceneRegistry(_sceneLibrary);

        }
        public ISceneDefinition ResolveScene(SceneId sceneId)
        {
            ISceneDefinition sceneData = _sceneRegistry.Resolve(sceneId);
            // Debug.Log($"Resolved {sceneId} to {sceneData.Tag.LevelName}");
            return sceneData;
        }

        public IEnumerator LoadSceneRoutine(ISceneDefinition sceneData)
        {
            // Start async load
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneData.Tag.LevelName);
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