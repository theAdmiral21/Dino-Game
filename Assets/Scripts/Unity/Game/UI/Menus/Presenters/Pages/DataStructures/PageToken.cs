using Game.Application.UI.Menus.Abstractions;
using UnityEngine;
namespace Game.UI.Menus.Unity.Presenters.Pages.DataStructures
{
    [CreateAssetMenu(menuName = "Game/UI/Page Token")]
    public class PageToken : ScriptableObject, IPageToken
    {
        public string PageName => Name;
        public string Name;
    }
}