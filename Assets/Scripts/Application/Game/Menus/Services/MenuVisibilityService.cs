using System;
using Primitives.Common.Menus.Enums;
using Primitives.Common.Menus.Services;

namespace Game.Application.Menus.Services
{
    public class MenuVisibilityService : IMenuVisibilityService
    {
        public event Action<MenuVisibility> VisibilityChanged;

        public void HideMenu()
        {
            throw new NotImplementedException();
        }

        public void ShowMenu()
        {
            throw new NotImplementedException();
        }
    }
}