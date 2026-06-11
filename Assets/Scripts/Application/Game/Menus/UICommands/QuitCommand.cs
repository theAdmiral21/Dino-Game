using System;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct QuitCommand : IUICommand
    {
        public Type CommandType => typeof(QuitCommand);



    }
}