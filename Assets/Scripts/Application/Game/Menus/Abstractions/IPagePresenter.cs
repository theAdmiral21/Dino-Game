using Game.Core.UI.Menus.Abstractions;

namespace Game.Application.UI.Menus.Abstractions
{
    public interface IPagePresenter : IToggleVisible, IUIPageTransisiton
    {
        public void ConfigurePage();
        public IUIElement[] Elements { get; }
        public IPageToken Token { get; }
        public bool IsGrid { get; }
        public int Rows { get; }
        public int Cols { get; }
    }
}