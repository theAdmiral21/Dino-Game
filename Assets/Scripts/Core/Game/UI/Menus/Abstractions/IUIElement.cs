using System;
using Primitives.Menus.Commands;

namespace Game.Core.UI.Menus.Abstractions
{
    public interface IUIElement
    {
        public Type PresenterType { get; }
        public event Action<IUICommand> OnSubmit;
        public event Action<IUICommand> OnFocus;
        public void Submit();
        public void Select();
        public void Deselect();
        public void Enable();
        public void Disable();
        public void Activate();
        public void Deactivate();
    }
}