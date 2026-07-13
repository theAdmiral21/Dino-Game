using Game.Application.Scenes.Abstractions;
using Game.Application.UI.Menus.Abstractions;
using Game.Core.Scenes;
using Game.Core.Scenes.Enums;
using Game.UI.Menus.Unity.Presenters.Pages.DataStructures;
using Primitives.Common.Scenes;
using Primitives.GameState;
using UnityEngine;

namespace Game.Unity.Scenes.DataStructures
{
    [CreateAssetMenu(fileName = "NameSceneContext", menuName = "Game/Scenes/MenuSceneContext")]
    public class MenuSceneContext : ScriptableObject, ISceneDefinition, IMenuSceneContext
    {
        public IPageToken RootPage => _rootPage;

        public SceneId Id => throw new System.NotImplementedException();

        public string SceneName => throw new System.NotImplementedException();

        public GameState StartState => throw new System.NotImplementedException();

        public SceneType TypeOfScene => throw new System.NotImplementedException();

        [SerializeField] private PageToken _rootPage;

    }
}