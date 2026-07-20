using System;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct InputIntCommand : IUICommand
    {
        public Type CommandType => typeof(InputIntCommand);
        public int IntValue { get; private set; }
        public InputIntCommand(int value)
        {
            IntValue = value;
        }

    }
}