using Gameplay.Common.Core.Abstractions;

namespace Gameplay.Common.Core.DataStructures
{
    public struct DairyDelightContext : IPickUpIndex
    {
        public int Points { get; private set; }
        public int Index { get; private set; }
        public DairyDelightContext(int points, int index)
        {
            Points = points;
            Index = index;
        }

    }
}