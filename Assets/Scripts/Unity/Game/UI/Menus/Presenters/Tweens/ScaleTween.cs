using DG.Tweening;
using Game.UI.Menus.Unity.Presenters.Abstractions;
using UnityEngine;


namespace Game.UI.Menus.Unity.Presenters.Tweens
{
    /// <summary>
    /// Wrapper class for playing a scale change tween. Note that for the public demo, there will not be any tweens. This will simplify the demo and keep people from having to download the tweening package (even though I absolutely love DOTween.)
    /// </summary>
    public class ScaleTween : UITween
    {
        [Header("Tween settings")]
        [SerializeField] private Transform _tweenTarget;
        [SerializeField] private Vector3 _finalValue;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isTo = true;
        [SerializeField] private Ease _easing = Ease.OutCubic;

        public override Tween BuildTween()
        {
            // Make the tween
            if (_isTo)
            {
                TweenObj = _tweenTarget.DOScale(_finalValue, _duration).SetEase(_easing);
            }
            else
            {
                TweenObj = _tweenTarget.DOScale(_finalValue, _duration).From().SetEase(_easing);
            }
            return TweenObj;
        }

        public override void PlayForward()
        {
            TweenObj.PlayForward();

        }

        public override void PlayBackwards()
        {
            TweenObj.PlayBackwards();
        }

    }
}