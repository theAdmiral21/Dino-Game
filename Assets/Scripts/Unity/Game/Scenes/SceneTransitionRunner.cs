using UnityEngine;
using System;
using System.Collections;
using Game.Systems.Scnes.Application.Abstractions;
using Game.Scenes.Application;
using Game.Unity.Scenes.DataStructures;
using Primitives.Common.Scenes;
using Game.Core.Scenes;
using Infrastructure.Unity;

namespace Game.Unity.Scenes
{
    /// <summary>
    /// Class that interfaces with unity to change from one scene to another
    /// </summary>
    public sealed class SceneTransitionRunner : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _loadingScreenMono;
        private ILoadingScreenPresenter _loadingScreen;

        [SerializeField] private MonoBehaviour _sceneLoaderMono;
        public ISceneLoader SceneLoader => _sceneLoader;
        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _loadingScreen = _loadingScreenMono as ILoadingScreenPresenter;
            if (_loadingScreen == null) Debug.Log($"{name} does not have a loading screen presenter");

            _sceneLoader = _sceneLoaderMono as ISceneLoader;
            if (_sceneLoader == null)
            {
                Debug.LogError($"{name} does not have a scene loader.");
                return;
            }
        }

        public void RunSceneTransition(SceneId sceneId, Action onComplete)
        {
            ISceneDefinition sceneData = _sceneLoader.ResolveScene(sceneId);
            // SceneData sceneData = temp as SceneData;
            if (sceneData == null)
            {
                Debug.LogError($"SceneId {sceneId} resolved to {sceneData} but could not be converted to type SceneData.");
                return;
            }

            StartCoroutine(TransitionRoutine(sceneData, onComplete));
        }


        private IEnumerator TransitionRoutine(ISceneDefinition sceneData, Action onComplete)
        {
            // Show a loading screen
            _loadingScreen?.ShowLoading();
            RegistryGateway.ClearRegistries();
            yield return _sceneLoader.LoadSceneRoutine(sceneData);
            // Hide loading screen
            _loadingScreen?.HideLoading();

            // call custom completion event
            // ISceneContext context = GetSceneContext();
            // BaseSceneContext val = context as BaseSceneContext;
            onComplete?.Invoke();
        }

        // private SceneContext GetSceneContext()
        // {
        //     Debug.LogError($"You don't have you scene context fully defined. Either define it or remove it.");
        //     return new SceneContext();
        // }
    }
}