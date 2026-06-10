using System;
using Primitives.Common.Menus.Enums;

namespace Primitives.Common.Menus.Services
{
    public interface IMenuVisibilityService
    {
        public event Action<MenuVisibility> VisibilityChanged;

        public void ShowMenu();
        public void HideMenu();
    }
}