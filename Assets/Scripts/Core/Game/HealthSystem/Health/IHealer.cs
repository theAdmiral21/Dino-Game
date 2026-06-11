namespace Game.Core.Health
{
    public interface IHealer
    {
        public void GiveHealth(HealInfo info);
    }
}