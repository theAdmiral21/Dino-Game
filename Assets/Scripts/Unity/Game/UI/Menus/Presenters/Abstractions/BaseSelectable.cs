using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Core.UI.Menus.Abstractions;
using Game.Application.UI.Menus.UICommands;
using Game.UI.Menus.Unity.Effects.Abstractions;
using Primitives.Menus.Commands;
using Game.UI.Menus.Core.Enums;
using Primitives.Menus;

namespace Game.UI.Menus.Unity.Presenters.Abstractions
{
    public abstract class BaseSelectable : MonoBehaviour,
    IUIElement,
    IMouseInteractable,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
    {
        [SerializeField] protected Button _button;
        [SerializeField] private MonoBehaviour _commandProviderMono;

        private IUIElementEffect[] _effects;
        private ICommandProvider _commandProvider;
        private Sprite _originalSprite;

        public event Action<IUICommand> OnSubmit;
        public event Action<IUICommand> OnFocus;

        public abstract Type PresenterType { get; }

        protected virtual void Awake()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
                if (_button == null)
                {
                    Debug.LogError($"{name} requires a Button component.");
                }
            }
            if (_commandProviderMono != null)
            {
                // Debug.Log($"Provider mono: {_commandProviderMono.name}");
                _commandProvider = _commandProviderMono as ICommandProvider;
                if (_commandProvider == null)
                {
                    Debug.LogError($"{name} does not have a valid command provider.");
                }
            }
            _effects = GetComponents<IUIElementEffect>();
            _originalSprite = _button.image.sprite;

            // foreach (var effect in _effects)
            // {
            //     Debug.Log($"Got effects: {effect}");
            // }
        }

        protected void PlayEffects(Action<IUIElementEffect> call)
        {
            foreach (var effect in _effects)
            {
                call(effect);
            }
        }

        public void Submit()
        {
            if (_commandProvider != null)
            {
                OnSubmit?.Invoke(_commandProvider.CreateCommand());
                // Play tweens
                PlayEffects(e => e.OnSubmit());
            }
            else
            {
                Debug.LogError($"No command provider found for {this}. Nothing will happen when you click.");
            }
            OnSubmitted();
        }

        // Optional hook for doing stuff after a button is clicked
        protected virtual void OnSubmitted() { }
        public virtual void Select()
        {
            if (_button != null)
            {
                if (_button.transition == Selectable.Transition.ColorTint)
                {
                    // Highlight button
                    _button.targetGraphic.color = _button.colors.highlightedColor;
                }
                else if (_button.transition == Selectable.Transition.SpriteSwap)
                {
                    _button.image.sprite = _button.spriteState.highlightedSprite;
                }
                // Play tweens
                PlayEffects(e => e.OnSelect());
            }
        }
        public virtual void Deselect()
        {
            if (_button != null)
            {
                if (_button.transition == Selectable.Transition.ColorTint)
                {
                    // Remove highlight
                    _button.targetGraphic.color = _button.colors.normalColor;
                }
                else if (_button.transition == Selectable.Transition.SpriteSwap)
                {
                    _button.image.sprite = _originalSprite;
                }

                // Play tweens
                PlayEffects(e => e.OnDeselect());
            }
        }


        public virtual void Enable()
        {
            if (_button != null)
            {
                _button.enabled = true;
                // Play tweens
                PlayEffects(e => e.OnEnable());
            }
        }

        public virtual void Disable()
        {
            if (_button != null)
            {
                _button.enabled = false;
                // Play tweens
                PlayEffects(e => e.OnDisable());
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            Submit();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log($"Pointer entered: {this.name}");
            Select();
            OnFocus?.Invoke(new UIFocusCommand(this, InputProviderType.Mouse));
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Deselect();
        }

        public void OnMouseEnter()
        {
            Select();
            OnFocus?.Invoke(new UIFocusCommand(this, InputProviderType.Mouse));
        }

        public void OnMouseExit()
        {
            Deselect();
        }

        public void OnMouseClick()
        {
            Submit();
        }


    }
}