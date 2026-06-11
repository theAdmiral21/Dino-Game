using System;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct ValueChangeCommand : IUICommand
    {
        public Type CommandType => typeof(ValueChangeCommand);

        public readonly float NewValue;
        public ValueChangeCommand(float newValue)
        {
            NewValue = newValue;
        }

    }
}