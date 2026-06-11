using System.Collections.Generic;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Input.Enums;

namespace Game.Application.UI.Menus.Navigation
{
    public class LinearNavigation : IUINavigationModel
    {
        public int Count => _count;
        private int _count;

        public LinearNavigation(IReadOnlyList<IUIElement> elements)
        {
            Initialize(elements);
        }
        public int GetNextIndex(int currentNdx, UIInput input)
        {
            if (input == UIInput.Up || input == UIInput.Left)
                return (currentNdx - 1 + _count) % _count;

            if (input == UIInput.Down || input == UIInput.Right)
                return (currentNdx + 1 + _count) % _count;

            return currentNdx;
        }

        public void Initialize(IReadOnlyList<IUIElement> elements)
        {
            _count = elements.Count;
        }
    }
}