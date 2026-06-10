using System.Collections.Generic;
using Movement.Core.Movement.Abstractions;

namespace Movement.Core.Abstractions
{
    public interface IActionResultViewer
    {
        public List<IActionResult> ActionResults { get; }
    }
}