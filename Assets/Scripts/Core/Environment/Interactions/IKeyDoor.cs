using Core.Environment.Events;
using Primitives.Environment;

namespace Core.Environment.Interactions
{
    public interface IKeyDoor
    {
        public void TryUnlock(UnlockEvent key);
    }
}