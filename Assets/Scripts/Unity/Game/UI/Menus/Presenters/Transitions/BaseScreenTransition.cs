using System;
using System.Collections;
using Game.Core.UI.Menus.Transitions;
using Primitives.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Unity.UI.Menus.Presenters.Transitions
{
    public abstract class BaseScreenTransition : MonoBehaviour, IScreenTransition
    {
        [SerializeField] protected Material _shaderMaterial;
        [SerializeField] protected float _duration;
        public float Duration { get; protected set; }
        public event Action OnStart;
        public event Action OnFinish;

        public bool IsPlaying { get; private set; }

        public abstract ScreenTransitions TransitionType { get; }

        protected void Awake()
        {
            _shaderMaterial = new Material(_shaderMaterial); // create instance from asset
            GetComponent<Image>().material = _shaderMaterial; // assign instance to renderer
            Duration = _duration;
        }

        public IEnumerator PlayIn()
        {
            Debug.Log($"Playing in transition of type: {TransitionType}");

            yield return Play(0f, 1f);
        }

        public IEnumerator PlayOut()
        {
            Debug.Log($"Playing out transition of type: {TransitionType}");

            yield return Play(1f, 0f);
        }

        private IEnumerator Play(float start, float finish)
        {
            IsPlaying = true;
            float elapsedTime = 0f;

            OnStart?.Invoke();

            while (elapsedTime < Duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / Duration);

                float evalT = Mathf.Lerp(start, finish, t);

                Evaluate(evalT);
                yield return null;
            }
            Evaluate(finish);
            OnFinish?.Invoke();
            IsPlaying = false;
            Debug.Log($"Transition complete elapsed time: {elapsedTime}");
        }

        protected abstract void Evaluate(float t);
    }
}