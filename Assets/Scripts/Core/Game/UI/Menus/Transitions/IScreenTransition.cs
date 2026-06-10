using System;
using System.Collections;
using Primitives.Effects;

namespace Game.Core.UI.Menus.Transitions
{
    public interface IScreenTransition
    {
        public ScreenTransitions TransitionType { get; }
        public bool IsPlaying { get; }
        public event Action OnStart;
        public event Action OnFinish;
        public IEnumerator PlayIn();
        public IEnumerator PlayOut();
    }
}