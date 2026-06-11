using Game.Core.Events;
using UnityEngine;

namespace Game.Unity.Events
{
    /// <summary>
    /// Simple component that triggers an animation parameter. Is meant to be used with other components that require an animation when an action happens. eg BounceEntity
    /// </summary>
    public class AnimationFeedBack : MonoBehaviour, IEventFeedBack
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _triggerParameter;
        public void React()
        {
            _animator.SetTrigger(_triggerParameter);
        }
    }
}