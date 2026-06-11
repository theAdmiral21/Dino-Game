using Game.Application.UI.Menus.UICommands;
using Primitives.Menus.Commands;
using UnityEngine;

namespace Primitives.UI.Menus.Unity.Commands
{
    public class ResumeCommandProvider : MonoBehaviour, ICommandProvider
    {
        public IUICommand CreateCommand()
        {
            return new ResumeCommand();
        }
    }
}