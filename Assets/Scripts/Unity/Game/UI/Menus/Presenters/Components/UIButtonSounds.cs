using System;
using Game.UI.Menus.Unity.Effects.Abstractions;
using Game.Unity.Audio;
using UnityEngine;

namespace Game.UI.Menus.Unity.Presenters.Components
{
    public class UISelectSound : MonoBehaviour, IUIElementEffect
    {
        [SerializeField] private MenuAudioBridge _menuAudio;

        public Type ElementEffectType => typeof(UISelectSound);

        public void OnSelect()
        {
            // Debug.Log($"Playing sound effect");
            _menuAudio.PlaySelectSound();
        }
        public void OnDeselect() { }
        public void OnSubmit() => _menuAudio.PlaySubmitSound();
        public void OnEnable() { }
        public void OnDisable() { }
    }
}