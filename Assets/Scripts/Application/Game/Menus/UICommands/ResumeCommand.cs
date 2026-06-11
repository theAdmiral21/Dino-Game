using System;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct ResumeCommand : IUICommand
    {
        public Type CommandType => typeof(ResumeCommand);

    }
}