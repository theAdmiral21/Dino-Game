using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Environment.Core.Level;
using Primitives.Checkpoints;
using UnityEngine;

namespace Unity.Tools.DebugTools
{
    public class SpawnAt : MonoBehaviour
    {
        [Header("Toggle for spawning a debug player")]
        public bool SpawnDebugPlayer
        {
            get => _spawnDebugPlayer;
            set => _spawnDebugPlayer = value;
        }
        [SerializeField] private bool _spawnDebugPlayer;

        [Header("The selected spawn point")]
        public CheckpointId SelectedSpawn;
        public static SpawnAt Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED SpawnAtTool {GetEntityId()}");
        }
        private void OnDestroy()
        {
            Debug.Log($"DESTROYED SpawnAtTool {GetEntityId()}");
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}