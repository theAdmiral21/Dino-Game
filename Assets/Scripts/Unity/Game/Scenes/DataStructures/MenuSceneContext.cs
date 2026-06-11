using Game.Application.Scenes.Abstractions;
using Game.Application.UI.Menus.Abstractions;
using Game.UI.Menus.Unity.Presenters.Pages.DataStructures;
using UnityEngine;

namespace Game.Unity.Scenes.DataStructures
{
    [CreateAssetMenu(fileName = "NameSceneContext", menuName = "Game/Scenes/MenuSceneContext")]
    public class MenuSceneContext : BaseSceneContext, IMenuSceneContext
    {
        public IPageToken RootPage => _rootPage;

        [SerializeField] private PageToken _rootPage;

    }
}