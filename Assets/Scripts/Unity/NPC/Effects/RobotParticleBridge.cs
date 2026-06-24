using System;
using System.Collections.Generic;
using Game.Core.Effects;
using NPC.Core.Effects;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public class RobotParticleBridge : MonoBehaviour, IParticleBridge
    {
        [SerializeField] private List<ParticleSystem> _particleSystems;
        private bool _played;
        public void ApplyEffect(IEffectResult effect)
        {
            switch (effect)
            {
                case DeathEffect death:
                    {
                        Debug.Log($"Animating death effect!");
                        PlayDeathParticles();
                        break;
                    }
            }
        }

        private void PlayDeathParticles()
        {
            if (_played) return;
            for (int i = 0; i < _particleSystems.Count; i++)
            {
                _particleSystems[i].Play();
            }
            _played = true;
        }
    }
}