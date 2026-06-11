namespace Gameplay.Common.Core.DataStructures
{
    public struct HealthPickUpContext
    {
        public int HealthAmount;
        public HealthPickUpContext(int healAmount) => HealthAmount = healAmount;
    }
}