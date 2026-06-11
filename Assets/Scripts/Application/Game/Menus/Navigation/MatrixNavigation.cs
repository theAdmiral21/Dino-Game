using System.Collections.Generic;
using Game.Core.UI.Menus.Abstractions;
using Primitives.Input.Enums;

namespace Game.Application.UI.Menus.Navigation
{
    public class MatrixNavigation : IUINavigationModel
    {
        public int RowCount => _rowCount;
        private int _rowCount;
        public int ColCount => _colCount;
        private int _colCount;

        public MatrixNavigation(IReadOnlyList<IUIElement> elements, int rows, int columns)
        {
            _rowCount = rows;
            _colCount = columns;
            Initialize(elements);
        }
        public int GetNextIndex(int currentNdx, UIInput input)
        {
            int row = currentNdx / _colCount;
            int col = currentNdx % _colCount;
            if (input == UIInput.Up) col--;

            if (input == UIInput.Down) col++;

            if (input == UIInput.Left) row--;

            if (input == UIInput.Right) row++;

            if (input == UIInput.Back || input == UIInput.Select || input == UIInput.None) return currentNdx;

            row = (row + _rowCount) % _rowCount;
            col = (col + _colCount) % _colCount;
            return row * _colCount + col;
        }

        public void Initialize(IReadOnlyList<IUIElement> elements)
        {
            // This doesn't do too much here, but by dog it is in the interface.
        }
    }
}