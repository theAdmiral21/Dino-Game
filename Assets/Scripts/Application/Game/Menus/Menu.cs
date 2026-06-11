using System.Collections.Generic;
using Game.Application.UI.Menus.Abstractions;
using Game.Core.UI.Menus.Abstractions;

namespace Game.Application.UI.Menus
{
    public class Menu
    {
        public List<IUIElement> Elements { get; private set; } = new();
        public int SelectedIndex { get; private set; } = 0;

        private IMenuPage _currentPage;

        public void AddElement(IUIElement element) => Elements.Add(element);

        public void Navigate(int direction)
        {
            if (Elements.Count == 0) return;
            // Deselect the current button
            Elements[SelectedIndex].Deselect();

            // Wrap the index
            SelectedIndex = (SelectedIndex + direction + Elements.Count) % Elements.Count;

            // Select the next button
            Elements[SelectedIndex].Select();
        }

        public void ActivateCurrent()
        {
            Elements[SelectedIndex].Activate();
        }
    }
}