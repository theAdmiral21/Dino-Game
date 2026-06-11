using System;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct SubmitCommand : IUICommand
    {
        public Type CommandType => typeof(SubmitCommand);

        public IUIElement SourceElement => _sourceElement;
        private IUIElement _sourceElement;

        public SubmitCommand(IUIElement source)
        {
            _sourceElement = source;
        }

    }
}