using UnityEngine;
using System;
using Game.Application.UI.Menus.Abstractions;
using Game.Core.UI.Menus.Abstractions;
using Game.UI.Menus.Unity.Presenters.Pages.DataStructures;
using Game.UI.Menus.Unity.Presenters.Abstractions;
using DG.Tweening;
using System.Linq;

namespace Game.UI.Menus.Unity.Presenters.Pages
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPagePresenter : MonoBehaviour, IPagePresenter
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        public IUIElement[] Elements => _elements;
        private IUIElement[] _elements;

        public IPageToken Token => _token;
        [SerializeField] private PageToken _token;

        public bool IsGrid => isGrid;
        [SerializeField] private bool isGrid;

        public int Rows => rows;
        [SerializeField] private int rows;

        public int Cols => cols;

        [SerializeField] private int cols;

        [SerializeField] private UITween[] _enterTweens;
        [SerializeField] private UITween[] _exitTweens;

        public void ConfigurePage()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            GetUIElements();
            if (_elements.Length == 0)
            {
                Debug.LogError($"No elements added to {name}");
            }
        }

        public void Hide()
        {
            Debug.Log($"Hiding page {_token.PageName}");

            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            Debug.Log($"Showing page {_token.PageName}");
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            // Select the first or default element
            _elements.FirstOrDefault().Select();
        }

        private void GetUIElements()
        {
            _elements = GetComponentsInChildren<IUIElement>();
            // Debug.Log($"Found {_elements.Length} elements in {name}");
        }

        public void PlayEnterTransition(Action onEnterComplete)
        {
            if (_enterTweens.Length > 0)
            {
                var enterSeq = DOTween.Sequence();
                foreach (var tween in _enterTweens)
                {
                    Debug.Log($"Added {tween.TweenName} to enter sequence");
                    enterSeq.Append(tween.BuildTween());
                }
                enterSeq.OnComplete(() => onEnterComplete.Invoke());
                if (enterSeq != null)
                {
                    Debug.Log("Playing enter sequence");
                    enterSeq.Play();
                }
            }
            else
            {
                onEnterComplete.Invoke();
            }
        }

        public void PlayExitTransition(Action onExitComplete)
        {
            if (_exitTweens.Length > 0)
            {
                var exitSeq = DOTween.Sequence();
                foreach (var tween in _exitTweens)
                {
                    Debug.Log($"Added {tween.TweenName} to exit sequence");
                    exitSeq.Append(tween.BuildTween());
                }
                exitSeq.OnComplete(() => onExitComplete.Invoke());
                if (exitSeq != null)
                {
                    Debug.Log("Playing exit sequence");
                    exitSeq.Play();
                }
            }
            else
            {
                onExitComplete.Invoke();
            }
        }
    }
}