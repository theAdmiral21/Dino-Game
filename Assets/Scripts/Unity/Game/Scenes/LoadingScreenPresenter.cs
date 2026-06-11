using UnityEngine;
using System.Collections;
using Game.Systems.Scnes.Application.Abstractions;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Game.Unity.Scenes
{
    /// <summary>
    /// Class that interfaces with unity to change from one scene to another
    /// </summary>
    public sealed class LoadingScreenPresenter : MonoBehaviour, ILoadingScreenPresenter
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _tennisBall;
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private RectTransform _stars;
        private Tween _ballStartTween;
        private Tween _ballSpinTween;
        private Tween _canvasFadeInTween;
        private Tween _loadingTextTween;
        private Tween _starScroll;
        private Tween _canvasFadeOutTween;
        private void Awake()
        {
            DOTween.Init();
        }
        public void HideLoading()
        {
            // Debug.Log("Hide loading screen");
            // Just in case this gets called by accident, only trigger it if we're visible
            if (_canvasGroup.alpha <= 0.9)
            {
                // _canvasFadeOutTween = _canvasGroup.DOFade(0f, 1f);
                _canvasFadeOutTween.SetEase(Ease.OutQuad);
                _canvasFadeOutTween.SetLoops(1);
                _canvasFadeOutTween.Play();
            }
        }

        public void ShowLoading()
        {
            // Debug.Log("Show loading screen");
            // StartCoroutine(TweenRoutine());
        }

        private IEnumerator TweenRoutine()
        {
            // _canvasFadeInTween = _canvasGroup.DOFade(1f, 1f).SetEase(Ease.OutQuad).SetLoops(1);
            _loadingTextTween.SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            yield return _canvasFadeInTween.WaitForCompletion();
            _ballStartTween = _tennisBall.transform.DORotate(new Vector3(0f, 0f, 45f), 1f).SetEase(Ease.OutQuad).SetRelative(true);
            yield return _ballStartTween.WaitForCompletion();
            _ballSpinTween = _tennisBall.transform.DORotate(new Vector3(0f, 0f, -360f), 0.05f).SetEase(Ease.InQuad).SetLoops(-1, LoopType.Incremental);
            _starScroll = _stars.transform.DOLocalMoveX(-1920, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }
}