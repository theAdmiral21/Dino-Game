using Movement.Core.Abstractions;

namespace Core.Movement.Abstractions
{
    public interface IActionRequestSinkProvider
    {
        public IActionRequestSink RequestSink { get; }
    }
}