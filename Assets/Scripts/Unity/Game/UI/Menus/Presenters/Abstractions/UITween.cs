using DG.Tweening;
using Game.Core.UI.Menus.Abstractions;
using UnityEngine;


namespace Game.UI.Menus.Unity.Presenters.Abstractions
{
    public abstract class UITween : MonoBehaviour, IUITween
    {
        public string TweenName;
        public Tween TweenObj;
        public virtual bool Blocking => true;
        public abstract Tween BuildTween();
        public abstract void PlayForward();
        public abstract void PlayBackwards();
    }

}
