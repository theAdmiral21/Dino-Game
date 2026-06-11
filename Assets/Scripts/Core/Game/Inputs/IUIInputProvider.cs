using System;
using Primitives.Input.Enums;
using UnityEngine;

namespace Game.Core.Inputs
{
    public interface IUIInputProvider
    {
        public Vector2 Navigate { get; }
        public event Action<UIInput> Select;
        public event Action<UIInput> Back;
        public float LastUsedTime { get; }
    }
}