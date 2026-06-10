using System;

namespace Primitives.Menus.Commands
{
    public interface IUICommand
    {
        public Type CommandType { get; }
    }

}