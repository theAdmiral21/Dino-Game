using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.Environment
{
    public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform cardRect;
        [SerializeField] private RectTransform canvasRect; // the parent Canvas's RectTransform
        [SerializeField] private Canvas canvas;             // needed for screen-to-local conversion

        [Header("Anchor / Return Position")]
        [SerializeField] private Vector2 restingAnchoredPos; // bottom-right corner slot

        [Header("Card Reader")]
        [SerializeField] private CardSwipeController swipeController;
        [SerializeField] private RectTransform readerSnapZone; // where the card must align first
        [SerializeField] private float snapDistance = 40f;
        private bool _snapped;
        private Vector2 _dragOffset;
        private void Awake()
        {
            cardRect.anchoredPosition = restingAnchoredPos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _snapped = false;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

            _dragOffset = cardRect.anchoredPosition - localPoint;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

            cardRect.anchoredPosition = localPoint + _dragOffset;

            if (!_snapped && Vector2.Distance(cardRect.anchoredPosition, readerSnapZone.anchoredPosition) <= snapDistance)
            {
                SnapToReader();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_snapped)
            {
                // Card is locked into the reader — begin the swipe-speed check
                swipeController.BeginSwipe(cardRect.anchoredPosition);
            }
            else
            {
                // Didn't align — whip back to corner
                ReturnToRest();
            }
        }

        private void SnapToReader()
        {
            _snapped = true;
            cardRect.anchoredPosition = readerSnapZone.anchoredPosition;
            // TODO: play snap sound / animation here
        }

        public void ReturnToRest()
        {
            _snapped = false;
            cardRect.anchoredPosition = restingAnchoredPos;
            // TODO: play whip-back animation here
        }
    }
}