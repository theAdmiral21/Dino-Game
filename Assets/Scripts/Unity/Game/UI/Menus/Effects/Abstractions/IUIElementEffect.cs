using System;

namespace Game.UI.Menus.Unity.Effects.Abstractions
{
    public interface IUIElementEffect
    {
        public Type ElementEffectType { get; }
        public void OnSelect();
        public void OnDeselect();
        public void OnSubmit();
        public void OnEnable();
        public void OnDisable();
    }
}