using System;
using DG.Tweening;
using Game.UI.Menus.Unity.Effects.Abstractions;
using UnityEngine;

namespace Game.UI.Menus.Unity.Presenters.Components
{
    /// <summary>
    ///  Class for playing a tween when the player hovers over a UI button. Note that the demo has no tweens for simplicity sake and because I didn't want to make people download DoTween (even though it is by far my favorite package).
    /// </summary>
    public class UIHoverTween : MonoBehaviour, IUIElementEffect
    {
        // private object _hoverTween;

        [SerializeField] private Tween _hoverTween;

        public Type ElementEffectType => typeof(UIHoverTween);

        private void Start()
        {
            DOTween.Init();
            DOTween.defaultAutoKill = false;
            DOTween.defaultRecyclable = true;
            _hoverTween = gameObject.transform.DOScale(Vector3.one * 2.5f, 0.5f).Pause();
        }
        public void OnSelect()
        {
            _hoverTween.PlayForward();
        }
        public void OnDeselect()
        {
            _hoverTween.PlayBackwards();
        }
        public void OnSubmit() { }
        public void OnEnable() { }
        public void OnDisable() { }
    }
}