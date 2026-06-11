using System;
using UnityEngine;

namespace Primitives.Core.Input.Abstractions
{
    public interface IInputSource
    {
        public bool IsEnabled { get; }
        public void Activate();
        public void Deactivate();
    }

    public interface IPlayerInputSource : IInputSource
    {
        public event Action BufferJump;
        public event Action JumpCancel;
        public event Action<Vector2> Run;
        public event Action<Vector2> RunStart;
        public event Action<Vector2> RunStop;
        public event Action Bark;
    }
}