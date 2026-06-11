namespace Game.Core.Health
{
    public interface IHealable
    {
        public void Heal(HealInfo info);
    }
}