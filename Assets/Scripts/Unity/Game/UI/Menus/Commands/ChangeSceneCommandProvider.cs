
using Game.Application.UI.Menus.UICommands;
using Primitives.Common.Scenes;
using Primitives.Menus.Commands;
using UnityEngine;

namespace Game.UI.Menus.Unity.Commands
{
    public class ChangeSceneCommandProvider : MonoBehaviour, ICommandProvider
    {
        public SceneId RequestedScene;
        public IUICommand CreateCommand()
        {
            return new ChangeSceneCommand(RequestedScene);
        }
    }
}