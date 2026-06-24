namespace Core.Equipment
{
    public interface IMagazine
    {
        public int RoundCount { get; }
        public int Capacity { get; }
        public void ReplenishRounds(int bulletCount);
        public bool ConsumeRound();
    }
}