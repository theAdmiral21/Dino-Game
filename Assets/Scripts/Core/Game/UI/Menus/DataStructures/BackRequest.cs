using System;
using Game.Core.UI.Menus.Abstractions;

namespace Game.Core.UI.Menus.DataStructures
{
    public struct BackRequest : IMenuRequest
    {
        public Type RequestType => typeof(BackRequest);
    }
}