using UnityEngine;
using Game.Application.UI.Menus.UICommands;
using Primitives.Menus.Commands;
using Game.UI.Menus.Unity.Presenters.Pages.DataStructures;

namespace Game.UI.Menus.Unity.Commands
{
    public class ChangePageCommandProvider : MonoBehaviour, ICommandProvider
    {
        [SerializeField] private PageToken _token;
        public IUICommand CreateCommand()
        {
            return new ChangePageCommand(_token);
        }
    }
}