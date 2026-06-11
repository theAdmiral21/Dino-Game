using Game.UI.Menus.Core.Enums;
using Primitives.UI.Menus.Unity.InputProviders;
using UnityEngine;

namespace Primitives.UI.Menus.Unity.Presenters
{
    public class CursorPresenter : MonoBehaviour
    {
        [SerializeField] private InputProviders.InputProvider _inputProvider;
        [SerializeField] private MouseInputProvider _mouseInput;

        public bool CursorIsVisible => _cursorIsVisible;
        private bool _cursorIsVisible;

        public InputProviderType CurrentInputProvider => _currentInputProvider;
        private InputProviderType _currentInputProvider;

        private void Update()
        {
            SetFocusDevice(_mouseInput.LastUsedTime, _inputProvider.LastUsedTime);
        }

        private void SetFocusDevice(float lastMouseTime, float keyboardGamepadTime)
        {
            if (keyboardGamepadTime > lastMouseTime)
            {
                Cursor.visible = false;
                _cursorIsVisible = false;
                _currentInputProvider = _inputProvider.CurrentProvider;

            }
            else
            {
                Cursor.visible = true;
                _cursorIsVisible = true;
                _currentInputProvider = InputProviderType.Mouse;
            }
        }
    }
}