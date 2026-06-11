using System;
using Game.UI.Menus.Core.Enums;
using Game.UI.Menus.Unity.Presenters.Abstractions;
namespace Primitives.UI.Menus.Unity.Presenters.Bindings
{
    [Serializable]
    public class UITweenBinding
    {
        public UITweenEvent Event;
        public UITween[] Tweens;
    }
}