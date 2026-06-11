using System;

namespace Game.Core.Health
{
    public struct HealInfo
    {
        public readonly int HealAmount;
        public readonly bool IsFullHeal;


        public HealInfo(int amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException();
            HealAmount = amount;
            IsFullHeal = false;
        }

        public HealInfo(bool fullHeal)
        {
            HealAmount = 0;
            IsFullHeal = fullHeal;
        }
    }
}