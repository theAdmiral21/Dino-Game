using System.Collections.Generic;
using System.Linq;
using Primitives.Checkpoints;
using Primitives.Common.Scenes;
using UnityEngine;

namespace Unity.Environment.Checkpoints.DataStructures
{
    [CreateAssetMenu(menuName = "Game/Checkpoints/Checkpoint Scene Map")]
    public class CheckpointMapSO : ScriptableObject
    {
        [SerializeField] private List<CheckpointSceneEntry> _entries = new();
        private Dictionary<SceneId, CheckpointSceneEntry> _map;

        private void OnEnable() => _map = _entries.ToDictionary(e => e.Scene, e => e);

        public CheckpointId GetStart(SceneId scene) => _map[scene].Start;
        public CheckpointId GetDebugStart(SceneId scene) => _map[scene].DebugStart;
    }

    [System.Serializable]
    public struct CheckpointSceneEntry
    {
        public SceneId Scene;
        public CheckpointId Start;
        public CheckpointId DebugStart;
    }

}