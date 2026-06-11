using UnityEngine;

namespace Primitives.UI.Menus.Unity.InputProvider
{
    public sealed class InputGate
    {
        float _last;
        readonly float _cooldown;

        public InputGate(float cooldown)
        {
            _cooldown = cooldown;
        }

        public bool Allow()
        {
            if (Time.unscaledTime - _last < _cooldown)
            {
                return false;
            }
            _last = Time.unscaledTime;
            return true;
        }
    }
}