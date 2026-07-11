using System;
using Game.UI.Menus.Unity.Effects.Abstractions;
using Game.Unity.Audio;
using NPC.Unity.Effects;
using UnityEngine;

namespace Game.UI.Menus.Unity.Presenters.Components
{
    public class UISelectSound : MonoBehaviour, IUIElementEffect
    {
        [SerializeField] private AudioBridge _menuAudio;

        public Type ElementEffectType => typeof(UISelectSound);

        public void OnSelect()
        {
            Debug.LogError($"Playing sound effect");
            // _menuAudio.PlaySelectSound();
        }
        public void OnDeselect() { }
        public void OnSubmit() { }// => _menuAudio.PlaySubmitSound();
        public void OnEnable() { }
        public void OnDisable() { }
    }
}