using Game.Core.Events;
using UnityEngine;

namespace Game.Unity.Events
{
    /// <summary>
    /// Simple component that triggers an animation parameter. Is meant to be used with other components that require an animation when an action happens. eg BounceEntity
    /// </summary>
    public class ParticleFeedBack : MonoBehaviour, IEventFeedBack
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public void React()
        {
            _particleSystem.Play();
        }
    }
}