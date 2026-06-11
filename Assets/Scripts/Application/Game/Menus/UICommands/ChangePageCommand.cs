using System;
using Game.Application.UI.Menus.Abstractions;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct ChangePageCommand : IUICommand
    {
        public Type CommandType => typeof(ChangePageCommand);
        public readonly IPageToken Token;
        public ChangePageCommand(IPageToken token) => Token = token;

    }
}