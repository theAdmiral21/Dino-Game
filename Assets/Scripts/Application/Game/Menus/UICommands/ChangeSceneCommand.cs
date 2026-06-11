using System;
using Primitives.Common.Scenes;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct ChangeSceneCommand : IUICommand
    {
        public Type CommandType => typeof(ChangeSceneCommand);
        public SceneId RequestedScene;

        public ChangeSceneCommand(SceneId sceneId) => RequestedScene = sceneId;
    }
}