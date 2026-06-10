using Movement.Core.Movement.DataStructures;

namespace Movement.Core.Abstractions
{
    public interface IActionRequestSink
    {
        public void EnqueueActionRequest(IActionRequest request);
    }
}