using Game.Core.Cinematics.Abstractions;
using Game.Core.Cinematics.Enums;

namespace Game.Core.Cinematics.DataStructures
{
    public struct CinematicRequest : ICinematicRequest
    {
        public CinematicId Id => _id;
        private CinematicId _id;

        public CinematicRequest(CinematicId id)
        {
            _id = id;
        }
    }
}