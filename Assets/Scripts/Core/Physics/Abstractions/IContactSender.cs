using System;

namespace Physics.Core.Abstractions
{
    public interface IContactSender
    {
        public Action React();
    }
}