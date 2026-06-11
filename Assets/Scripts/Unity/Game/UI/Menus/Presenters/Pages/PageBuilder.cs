using Game.Application.UI.Menus;
using Game.Application.UI.Menus.Abstractions;
using Game.Application.UI.Menus.Navigation;
using Game.Core.UI.Menus.Abstractions;
using UnityEngine;

namespace Game.UI.Menus.Unity.Presenters.Pages
{
    public class PageBuilder : MonoBehaviour, IPageBuilder
    {


        public IMenuPage Build()
        {
            IPagePresenter page = GetPagePresenter();
            ConfigurePresenter(page);
            Debug.Assert(page != null, $"{name} does not have a page presenter!");
            // Debug.Log($"Page: {page}");
            // Debug.Log($"Added {page.Elements.Length} elements");

            // Pick navigation model
            IUINavigationModel navModel = page.IsGrid
                ? new MatrixNavigation(page.Elements, page.Rows, page.Cols)
                : new LinearNavigation(page.Elements);

            // Create application-level page
            var menuPage = new MenuPage(page.Elements, navModel, page);
            return menuPage;
        }

        private IPagePresenter GetPagePresenter()
        {
            return GetComponent<IPagePresenter>();
        }

        private void ConfigurePresenter(IPagePresenter pagePresenter)
        {
            pagePresenter.ConfigurePage();
        }
    }
}