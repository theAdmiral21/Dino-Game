namespace Core.NPC.Attacks
{
    public interface IAttack
    {
        public IFrameAttack FrameAttack { get; }

        public void Swing();
    }
}