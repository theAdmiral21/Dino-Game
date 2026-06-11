using System;
using Game.Core.UI.Menus.Abstractions;
using Game.UI.Menus.Core.Enums;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct UIFocusCommand : IUICommand
    {
        public Type CommandType => typeof(UIFocusCommand);

        public IUIElement Element => _element;
        private IUIElement _element;
        public InputProviderType ProviderType => _provider;
        private InputProviderType _provider;

        public UIFocusCommand(IUIElement element, InputProviderType provider)
        {
            _element = element;
            _provider = provider;
        }

    }
}