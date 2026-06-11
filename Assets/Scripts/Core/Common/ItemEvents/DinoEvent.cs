namespace Gameplay.Common.Core.ItemEvents
{
    public record DinoEvent
    {
        public int DinoIndex { get; private set; }

        public DinoEvent(int dinoIndex) => DinoIndex = dinoIndex;
    }
}