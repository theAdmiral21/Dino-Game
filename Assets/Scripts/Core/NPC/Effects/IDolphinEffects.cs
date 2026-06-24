namespace NPC.Core.Effects
{
    public interface IDolphinEffects
    {
        public bool IsReady { get; }
        public void SummonDolphin();
        public void DismissDolphin();
        public void SetDolphinState(bool setReady);
    }
}