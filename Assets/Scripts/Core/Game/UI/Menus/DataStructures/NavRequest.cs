using System;
using Game.Core.UI.Menus.Abstractions;

namespace Game.Core.UI.Menus.DataStructures
{
    public struct NavRequest : IMenuRequest
    {
        public Type RequestType => typeof(NavRequest);
    }
}