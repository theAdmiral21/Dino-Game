using System;
using Movement.Core.Enums;
using Movement.Core.Movement.Abstractions;
using Primitives.Audio;

namespace Movement.Core.Movement.DataStructures
{
    /// <summary>
    /// This is where Core tells the requesting application level object the result of the jump.
    /// </summary>
    public struct LandingResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public readonly SurfaceType Surface => _surface;
        private SurfaceType _surface;
        public Type ResultType => typeof(JumpResult);

        public LandingResult(bool approved, SurfaceType surface, ActionPhase phase)
        {
            _approved = approved;
            _surface = surface;
            _phase = phase;
        }
    }
}