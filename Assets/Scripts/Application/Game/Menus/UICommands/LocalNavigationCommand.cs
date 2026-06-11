using System;
using Primitives.Input.Enums;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct LocalNavigationCommand : IUICommand
    {
        public Type CommandType => typeof(LocalNavigationCommand);
        public readonly UIInput Input;
        public LocalNavigationCommand(UIInput input) => Input = input;

    }
}