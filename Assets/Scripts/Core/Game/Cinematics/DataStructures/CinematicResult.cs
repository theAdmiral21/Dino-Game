using Game.Core.Cinematics.Enums;

namespace Game.Core.Cinematics.DataStructures
{
    public struct CinematicResult
    {
        public readonly bool Approved;
        public readonly CinematicId Cinematic;

        public CinematicResult(bool approved, CinematicId cinematic)
        {
            Approved = approved;
            Cinematic = cinematic;
        }
    }
}