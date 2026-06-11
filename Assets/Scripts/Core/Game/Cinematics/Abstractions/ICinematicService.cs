using System;
using Game.Core.Cinematics.Enums;

namespace Game.Core.Cinematics.Abstractions
{
    public interface ICinematicService
    {
        CinematicId Id { get; }
        public void PlayCinematic(Action onFinished);
    }
}