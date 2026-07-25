using Core.Environment.Interactions;
using Environment.Core.Interactions;
using Game.Unity.Events;
using UnityEngine;

namespace Unity.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class Door : MonoBehaviour, IInteractable, IDoor
    {
        [Header("Feedback components")]
        [SerializeField] private AudioFeedBack _unlockAudio;
        [SerializeField] private AudioFeedBack _lockAudio;
        [SerializeField] private AudioFeedBack _openAudio;
        [SerializeField] private AudioFeedBack _closeAudio;
        [SerializeField] private AudioFeedBack _jiggleHandleSound;
        [SerializeField] private AnimationFeedBack _openAnimation;
        [SerializeField] private AnimationFeedBack _closeAnimation;
        [Header("Required colliders")]
        [SerializeField] private Collider2D _doorCollider;
        [SerializeField] private Collider2D _interactionCollider;
        [Header("Door Default Values")]
        [SerializeField] private bool _isLocked;
        [SerializeField] private bool _isOpen;


        public bool IsLocked { get; private set; }

        public bool IsOpen { get; private set; }

        protected virtual void Awake()
        {
            IsOpen = _isOpen;
            IsLocked = _isLocked;

            _interactionCollider.enabled = !IsLocked;
        }

        public virtual bool CanInteract()
        {
            if (_jiggleHandleSound != null) _jiggleHandleSound.React();
            return !IsLocked;
        }

        public void Interact()
        {
            Debug.Log($"Interaction with door");
            if (CanInteract())
            {
                if (IsOpen)
                {
                    if (_closeAudio != null) _closeAudio.React();
                    if (_closeAnimation != null) _closeAnimation.React();

                    CloseDoor();
                }
                else
                {
                    if (_openAudio != null) _openAudio.React();
                    if (_openAnimation != null) _openAnimation.React();

                    OpenDoor();
                }
            }
        }

        public virtual void SetLocked(bool val)
        {
            if (val == IsLocked) return;

            IsLocked = val;

            if (IsLocked)
            {
                Debug.Log($"Locking door");
                _lockAudio.React();
                _interactionCollider.enabled = false;
            }
            else
            {
                Debug.Log($"Unlocking door");
                _unlockAudio.React();
                _interactionCollider.enabled = true;
            }
        }

        private void OpenDoor()
        {
            IsOpen = true;
            _doorCollider.gameObject.SetActive(false);
            Debug.Log($"Opened door");
        }

        private void CloseDoor()
        {
            IsOpen = false;
            _doorCollider.gameObject.SetActive(true);
            Debug.Log($"Closed door");
        }
    }
}