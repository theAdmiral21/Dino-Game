using DG.Tweening;
using Game.Unity.Events;
using NPC.Core.Effects;
using UnityEngine;

namespace NPC.Unity.Effects
{
    public class DolphinEffects : MonoBehaviour, IDolphinEffects
    {

        [SerializeField] private Transform _dest;
        [SerializeField] private float _summonTime;
        [SerializeField] private GameObject _interactCue;
        [SerializeField] private AudioFeedBack _audioFeedback;
        private Tween _summonTween;

        public bool IsReady { get; private set; }
        private void Awake()
        {
            DOTween.Init();

            // _summonTween = transform.DOMove(_dest.position, _summonTime).Pause();
            _summonTween = transform
                            .DOMove(_dest.position, _summonTime)
                            .SetAutoKill(false)
                            .Pause();

            _summonTween.ForceInit();
            _summonTween.OnComplete(HandleSummonComplete);
            _summonTween.OnRewind(HandleDismissComplete);

            _interactCue.SetActive(false);
        }
        public void DismissDolphin()
        {
            // _summonTween.PlayBackwards();
            _summonTween.SmoothRewind();
            _audioFeedback.React();

            // Update visual to indicate readiness
            _interactCue.SetActive(false);
        }

        public void SummonDolphin()
        {
            _summonTween.Play();
            _audioFeedback.React();
        }

        private void HandleSummonComplete()
        {
            // Update visual to indicate readiness
            _interactCue.SetActive(true);
            IsReady = true;

        }

        private void HandleDismissComplete()
        {
            IsReady = false;
        }

        public void SetDolphinState(bool setReady)
        {
            if (setReady)
            {
                _summonTween.Complete();
                HandleSummonComplete();
            }
            else
            {
                _summonTween.Rewind();
                HandleDismissComplete();
            }
        }
    }
}