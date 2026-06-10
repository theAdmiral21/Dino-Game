using System;
using UnityEngine;
using Primitives.Input.Enums;

namespace Game.Core.UI.Menus.Abstractions
{
    public interface IUIInputProvider
    {
        public Vector2 Navigate { get; }
        public event Action<UIInput> Select;
        public event Action<UIInput> Back;
        public float LastUsedTime { get; }
    }
}