using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.Abstractions
{
    public interface IUICommandHandler
    {
        public void HandleCommand(IUICommand uiCommand);
    }
}