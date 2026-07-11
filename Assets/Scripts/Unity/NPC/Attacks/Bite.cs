using Application.NPC.Attacks;
using Core.Game.HealthSystem.Damage;
using Core.NPC.Attacks;
using Game.Core.Audio;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using NPC.Unity.Effects;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using Primitives.Damage;
using Unity.Tools.DrawingTools;
using UnityEngine;

namespace Unity.NPC.Attacks
{
    public class Bite : MonoBehaviour, IAttack
    {
        [SerializeField] BoxCollider2D _hitBox;

        public IFrameAttack FrameAttack { get; private set; }

        [SerializeField] AudioBridge _audioBridge;

        public DamageType DamageType;
        public Vector2 KnockBackVelocity;
        public float KnockBackApex;
        public float StunTime;
        public int DamageValue;
        [SerializeField] private LayerMask _layerMask;
        private void Awake()
        {
            // _layerMask = LayerMask.NameToLayer("Player");
            // _layerMask = LayerMask.GetMask("Player");
            FrameAttack = new FrameAttack(new DamageInfo(DamageType, KnockBackVelocity, KnockBackApex, StunTime, DamageValue));
        }

        public void Swing()
        {
            // Debug.Log($"Swing called");
            IDamageable damageable = GetDamageable();
            _audioBridge.PlaySound(EntityKey.Raptor, ActionSoundKey.Attack);
            // Debug.Log($"Found damageable: {damageable != null}");
            FrameAttack.TryAttack(damageable);

        }

        private IDamageable GetDamageable()
        {
            Vector2 center = _hitBox.bounds.center;
            Vector2 size = _hitBox.bounds.size;
            Collider2D overlap = Physics2D.OverlapBox(center, size, 0f, _layerMask);
            DrawUtil.DrawRectangle(center, size / 2, Color.red);
            if (overlap != null)
            {
                // Debug.Log($"Found collider: {overlap.name}");
                overlap.TryGetComponent<IDamageProvider>(out var damageProvider);
                if (damageProvider != null)
                {
                    return damageProvider.Damageable;
                }
            }
            return null;
        }
    }
}