using System.Collections.Generic;

namespace Core.Environment.Abstractions
{
    public interface IKeypadPresenter
    {
        public void UpdateDisplay(List<int> entry);
        public void ClearDisplay();
    }
}