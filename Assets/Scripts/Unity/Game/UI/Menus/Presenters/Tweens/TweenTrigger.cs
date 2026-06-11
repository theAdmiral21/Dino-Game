using System;
using Game.UI.Menus.Core.Enums;
using Game.UI.Menus.Unity.Effects.Abstractions;
using Primitives.UI.Menus.Unity.Presenters.Bindings;
using UnityEngine;


namespace Game.UI.Menus.Unity.Presenters.Tweens
{
    /// <summary>
    /// Mono class that plays a UITween when a certain UI event is raised. (Select/Deselect etc) This class can play a tween forwards and backwards. Submit, Select, and Enable events will play the tween forwards and their counter parts will play the tween backwards.
    /// </summary>
    public class TweenTrigger : MonoBehaviour, IUIElementEffect
    {
        [SerializeField] private UITweenBinding[] _bindings;

        public Type ElementEffectType => typeof(TweenTrigger);
        public void OnSubmit() => Play(UITweenEvent.Submit, true);
        public void OnSelect() => Play(UITweenEvent.Select, true);
        public void OnDeselect() => Play(UITweenEvent.Deselect, false);
        public void OnEnable() => Play(UITweenEvent.Enable, true);
        public void OnDisable() => Play(UITweenEvent.Disable, false);

        private void Play(UITweenEvent evt, bool forward)
        {
            foreach (var binding in _bindings)
            {
                if (binding.Event != evt) continue;

                foreach (var tween in binding.Tweens)
                {
                    if (tween == null) continue;

                    if (forward)
                    {
                        tween.PlayForward();
                    }
                    else
                    {
                        tween.PlayBackwards();
                    }
                }
            }
        }
    }
}