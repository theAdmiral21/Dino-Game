using System;

namespace Game.Core.Cinematics.Abstractions
{
    public interface ICinematicProvider
    {
        public Type CinematicType { get; }
    }
}