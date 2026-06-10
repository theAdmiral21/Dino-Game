using System.Collections.Generic;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Infrastructure.Unity
{
    public class SceneInitializationRegistry : MonoBehaviour, ISceneInitRegistry
    {
        // private static ISceneBootStrapper _bootStrapper;

        [Header("Debug")]
        [SerializeField]
        private Registry<IInitializable<IGameContext>> _systems = new();
        public IReadOnlyCollection<IInitializable<IGameContext>> Systems => _systems.Entities;

        private void Awake()
        {
            Debug.Log($"SceneInitializationRegistry awake - Frame: {Time.frameCount}");
            RegistryGateway.SetRegistry(_systems);
        }

    }
}