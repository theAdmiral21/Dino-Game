using System.Collections.Generic;
using Movement.Core.Movement.DataStructures;

namespace Movement.Core.Abstractions
{
    public interface IActionRequestHandler
    {
        public List<IActionRequest> ActionRequests { get; }
        public void UpdateRequestList(IActionRequest newRequest);
        public void ClearRequestList();
    }
}