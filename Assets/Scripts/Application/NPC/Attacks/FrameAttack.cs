using Core.NPC.Attacks;
using Primitives.Damage;
using Primitives.Physics;

namespace Application.NPC.Attacks
{
    public class FrameAttack : IFrameAttack
    {
        public DamageInfo Damage { get; private set; }
        private bool _fired;
        public FrameAttack(DamageInfo damage)
        {
            Damage = damage;
        }

        public bool TryAttack(IDamageable target)
        {
            if (target == null) return false;
            if (_fired) return false;
            target.ReceiveDamage(Damage);
            _fired = true;
            // We made contact
            return true;
        }

        public void Reset() => _fired = false;
    }
}