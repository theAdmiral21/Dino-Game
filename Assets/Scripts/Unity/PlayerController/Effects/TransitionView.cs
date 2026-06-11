using System.Collections;
using System.Collections.Generic;
using Game.Core.UI.Menus.Transitions;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Effects;
using UnityEngine;

namespace PlayerController.Unity.Effects
{
    public class TransitionView : MonoBehaviour, ITransitionView
    {
        [SerializeField] private List<MonoBehaviour> _screenTransitions = new();

        public Dictionary<ScreenTransitions, IScreenTransition> Transitions => _transitions;
        private Dictionary<ScreenTransitions, IScreenTransition> _transitions = new();

        private void Awake()
        {

            // Add all of the transitions
            foreach (var transition in _screenTransitions)
            {
                // Cast the transition
                IScreenTransition screenTransition = transition as IScreenTransition;
                if (screenTransition != null)
                {
                    // Debug.Log($"Added transition: {screenTransition.TransitionType}");
                    _transitions[screenTransition.TransitionType] = screenTransition;
                }
            }
        }

        public IEnumerator PlayInTransition(ScreenTransitions transition)
        {
            yield return StartCoroutine(_transitions[transition].PlayIn());
        }

        public IEnumerator PlayOutTransition(ScreenTransitions transition)
        {
            yield return StartCoroutine(_transitions[transition].PlayOut());
        }
    }
}