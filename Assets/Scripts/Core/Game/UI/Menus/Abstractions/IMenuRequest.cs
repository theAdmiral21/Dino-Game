using System;

namespace Game.Core.UI.Menus.Abstractions
{
    public interface IMenuRequest
    {
        Type RequestType { get; }
    }
}