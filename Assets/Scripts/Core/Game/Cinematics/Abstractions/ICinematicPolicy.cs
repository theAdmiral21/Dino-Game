using Game.Core.Cinematics.DataStructures;
using Game.Core.Cinematics.Enums;

namespace Game.Core.Cinematics.Abstractions
{
    public interface ICinematicPolicy
    {
        public CinematicResult Evaluate(CinematicState current, ICinematicRequest request);
    }
}