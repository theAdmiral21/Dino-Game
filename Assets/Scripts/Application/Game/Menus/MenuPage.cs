using System;
using Game.Application.UI.Menus.Abstractions;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Input.Enums;

namespace Game.Application.UI.Menus
{
    public class MenuPage : IMenuPage
    {
        public IUIElement[] Elements => _elements;
        private IUIElement[] _elements;

        public IUINavigationModel NavigationModel => _navModel;
        private IUINavigationModel _navModel;

        public IPagePresenter PagePresenter => _pagePresenter;
        private IPagePresenter _pagePresenter;

        public IUIElement CurrentElement => Elements[_currentNdx];

        private int _currentNdx;


        public MenuPage(IUIElement[] elements, IUINavigationModel navModel, IPagePresenter pagePresenter)
        {
            _elements = elements;
            _navModel = navModel;
            _pagePresenter = pagePresenter;

            //TODO Set up the scene controller to select this after everything is ready.
            // CurrentElement.Select();
        }

        public void Enter()
        {
            _pagePresenter.Show();
        }

        public void Exit()
        {
            _pagePresenter.Hide();
        }

        public void EnterFancy(Action onEnterComplete)
        {
            _pagePresenter.PlayEnterTransition(onEnterComplete);
        }

        public void ExitFancy(Action onExitComplete)
        {
            _pagePresenter.PlayExitTransition(onExitComplete);
        }

        public void ChangeSelection(UIInput input)
        {
            int ndx = NavigationModel.GetNextIndex(_currentNdx, input);
            UpdateUI(ndx);
        }
        public void SetSelection(int ndx)
        {
            UpdateUI(ndx);
        }
        private void UpdateUI(int nextNdx)
        {
            // Deselect the current object
            CurrentElement.Deselect();
            // Update the current object
            _currentNdx = nextNdx;
            // Select the current object
            CurrentElement.Select();
        }

    }
}