using System.Collections.Generic;
using Game.Core.Scenes;
using Primitives.Common.Scenes;
using UnityEngine;

namespace Game.Unity.Scenes.DataStructures
{
    [CreateAssetMenu(fileName = "SceneLibrary", menuName = "Game/Scenes/SceneLibrary")]
    public class SceneLibrary : ScriptableObject
    {
        public List<BaseSceneContext> sceneList;

        public ISceneDefinition GetScene(SceneId sceneName)
        {
            return sceneList.Find(scene => scene.Tag.Id == sceneName);
        }

        public ISceneDefinition GetScene(string sceneName)
        {
            return sceneList.Find(scene => scene.Tag.LevelName == sceneName);
        }
    }
}