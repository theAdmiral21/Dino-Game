using System;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct BackCommand : IUICommand
    {
        public Type CommandType => typeof(BackCommand);

        public event Action OnBack;

    }
}