using System.Collections.Generic;
using System.Linq;
using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Primitives.Checkpoints;
using Primitives.Common.Scenes;
using Primitives.GameState;
using Unity.Game.Scenes.DataStructures;
using UnityEngine;

namespace Unity.Environment.Checkpoints.DataStructures
{
    [CreateAssetMenu(menuName = "Game/Scenes/Scene Map")]
    public class SceneMapSO : ScriptableObject
    {
        [SerializeField] private List<SceneMapEntry> _entries = new();
        private Dictionary<SceneId, SceneMapEntry> _map;

        private void OnEnable() => _map = _entries.ToDictionary(e => e.Id, e => e);

        public string GetSceneName(SceneId id) => _map[id].SceneName;
        public SceneId GetSceneId(string sceneName)
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].SceneName == sceneName)
                {
                    return _entries[i].Id;
                }
            }
            Debug.LogError($"Unable to find a scene by the name: {sceneName}");
            return SceneId.None;
        }

        public ISceneDefinition GetSceneDefinition(SceneId id)
        {
            return new SceneDefinition
                (
                    id,
                    _map[id].SceneName,
                    _map[id].StartState,
                    _map[id].TypeOfScene
                );
        }

        public ISceneDefinition GetSceneDefinition(string sceneName)
        {
            // Get the id and call the base method
            SceneId id = GetSceneId(sceneName);
            return GetSceneDefinition(id);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                var entry = _entries[i];
                entry.Bake();
                _entries[i] = entry;
            }
        }
#endif
    }

    [System.Serializable]
    public struct SceneMapEntry
    {
        public SceneId Id;
        public GameState StartState;
        public SceneType TypeOfScene;
#if UNITY_EDITOR
        public UnityEditor.SceneAsset SceneAsset;
#endif
        [SerializeField, HideInInspector] private string _sceneName;
        public string SceneName => _sceneName;

#if UNITY_EDITOR
        public void Bake() => _sceneName = SceneAsset != null ? SceneAsset.name : null;
#endif
    }

}