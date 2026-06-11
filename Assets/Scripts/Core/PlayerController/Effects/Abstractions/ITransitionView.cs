using System.Collections;
using System.Collections.Generic;
using Game.Core.UI.Menus.Transitions;
using Primitives.Effects;

namespace PlayerController.Core.Effects.Abstractions
{
    public interface ITransitionView
    {
        public Dictionary<ScreenTransitions, IScreenTransition> Transitions { get; }

        public IEnumerator PlayInTransition(ScreenTransitions transition);
        public IEnumerator PlayOutTransition(ScreenTransitions transition);
    }
}