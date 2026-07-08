using Core.Environment.Interactions;
using Environment.Core.Interactions;
using Game.Unity.Events;
using NUnit.Framework;
using UnityEngine;

namespace Unity.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class Door : MonoBehaviour, IInteractable, IDoor
    {
        [Header("Option feedback components")]
        [SerializeField] private AudioFeedBack _openAudio;
        [SerializeField] private AudioFeedBack _closeAudio;
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

        private void Awake()
        {
            IsOpen = _isOpen;
            IsLocked = _isLocked;
        }

        public bool CanInteract()
        {
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

        public void SetLocked(bool val)
        {
            IsLocked = val;
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