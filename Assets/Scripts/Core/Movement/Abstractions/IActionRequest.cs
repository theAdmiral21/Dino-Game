using System;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public interface IActionRequest
    {
        Type RequestType { get; }
    };
}