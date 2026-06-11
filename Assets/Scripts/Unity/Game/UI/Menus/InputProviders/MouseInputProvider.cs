using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Game.UI.Menus.Unity.Presenters.Abstractions;

namespace Primitives.UI.Menus.Unity.InputProviders
{
    public class MouseInputProvider : MonoBehaviour
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private GraphicRaycaster _raycaster;

        private PointerEventData _pointerData;
        private IMouseInteractable _current;
        private Vector2 _lastPos;

        private readonly List<RaycastResult> _results = new List<RaycastResult>();

        public float LastUsedTime => _lastUsedTime;
        private float _lastUsedTime;

        private void Awake()
        {
            _pointerData = new PointerEventData(EventSystem.current);
        }

        private void Update()
        {
            _pointerData.position = Mouse.current.position.ReadValue();


            _results.Clear(); // reuse list
            _raycaster.Raycast(_pointerData, _results);

            IMouseInteractable hit = null;

            foreach (var result in _results)
            {
                hit = result.gameObject.GetComponent<IMouseInteractable>();
                if (hit != null)
                    break;
            }
            if (hit != _current)
            {
                _current?.OnMouseExit();
                hit?.OnMouseEnter();
                _current = hit;
            }

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _current?.OnMouseClick();
            }

            // Check if the mouse was moved last frame
            float delta = _lastPos.magnitude - _pointerData.position.magnitude;
            if (Mathf.Abs(delta) > 0.01f || Mouse.current.leftButton.wasPressedThisFrame)
            {
                _lastUsedTime = Time.unscaledTime;
            }

            _lastPos = _pointerData.position;
        }
    }
}