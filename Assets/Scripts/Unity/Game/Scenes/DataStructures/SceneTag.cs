using Game.Core.Scenes;
using Primitives.Common.Scenes;
using UnityEngine;

namespace Game.Unity.Scenes.DataStructures
{
    [CreateAssetMenu(fileName = "Scene-Name-Tag", menuName = "Game/Scenes/SceneTag")]
    public class SceneTag : ScriptableObject, ISceneTag
    {
        public string LevelName => _levelName;
        [SerializeField] private string _levelName;

        public SceneId Id => _id;
        [SerializeField] private SceneId _id;
    }
}