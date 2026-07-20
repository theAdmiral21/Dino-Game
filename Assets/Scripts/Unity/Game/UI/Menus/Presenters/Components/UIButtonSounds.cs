using System;
using Game.UI.Menus.Unity.Effects.Abstractions;
using Game.Unity.Audio;
using NPC.Unity.Effects;
using Primitives.Audio.EntityKeys;
using Primitives.Audio.SoundKeys;
using UnityEngine;

namespace Game.UI.Menus.Unity.Presenters.Components
{
    public class UISelectSound : MonoBehaviour, IUIElementEffect
    {
        [SerializeField] private AudioBridge _menuAudio;


        [SerializeField] EntityKey _entity;
        public Type ElementEffectType => typeof(UISelectSound);

        public void OnSelect()
        {
            // Debug.LogError($"Playing sound effect");
            _menuAudio.PlaySound(_entity, ActionSoundKey.Select);
        }
        public void OnDeselect() { }
        public void OnSubmit()
        {
            // Debug.LogError($"Playing sound effect");
            _menuAudio.PlaySound(_entity, ActionSoundKey.Submit);
        }
        public void OnEnable() { }
        public void OnDisable() { }
    }
}