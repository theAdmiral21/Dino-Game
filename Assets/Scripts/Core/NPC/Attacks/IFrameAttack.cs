using Primitives.Damage;

namespace Core.NPC.Attacks
{
    public interface IFrameAttack
    {
        public DamageInfo Damage { get; }

        public bool TryAttack(IDamageable target);

        public void Reset();
    }
}