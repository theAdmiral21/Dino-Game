using Primitives.Input;
using UnityEngine;

namespace PlayerController.Unity.Inputs
{
    public abstract class BaseInputReader : MonoBehaviour
    {
        public abstract InputContext Type { get; }
        public abstract bool IsActive { get; }

        // Input actions
        protected GameInputs _actions;

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void Initialize(GameInputs inputActions);

    }
}