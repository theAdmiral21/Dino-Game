using System;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Input.Enums;


namespace Game.Application.UI.Menus.Abstractions
{
    public interface IMenuPage
    {
        public IUIElement[] Elements { get; }
        public IUINavigationModel NavigationModel { get; }
        public IPagePresenter PagePresenter { get; }
        public IUIElement CurrentElement { get; }
        // Enter/Exit methods for revealing and hiding the current page
        public void Enter();
        public void Exit();
        // Enter/Exit methods for entering/exiting a page and playing the pages transition
        public void EnterFancy(Action onEnterComplete);
        public void ExitFancy(Action onExitComplete);
        public void ChangeSelection(UIInput input);
        public void SetSelection(int ndx);
    }
}