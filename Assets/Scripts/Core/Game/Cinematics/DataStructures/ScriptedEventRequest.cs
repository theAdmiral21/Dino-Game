using System;
using Game.Core.Cinematics.Abstractions;
using Game.Core.Cinematics.Enums;

namespace Game.Core.Cinematics.DataStructures
{
    public struct ScriptedEventRequest : ICinematicProvider
    {
        public Type CinematicType => typeof(ScriptedEventRequest);
        public ScriptedEventId Id;

    }
}