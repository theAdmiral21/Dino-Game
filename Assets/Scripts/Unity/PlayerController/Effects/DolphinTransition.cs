using Game.Unity.UI.Menus.Presenters.Transitions;
using Primitives.Easing;
using Primitives.Effects;
using UnityEngine;

namespace PlayerController.Unity.Effects.Transition
{
    public class DolphinTransition : BaseScreenTransition
    {


        private float _maxScale = 100;
        // Debug
        private float _currentScale;
        [SerializeField] private int _priority = 8;
        public int Priority => _priority;

        public override ScreenTransitions TransitionType => ScreenTransitions.Dolphin;

        // private void Awake()
        // {
        //     base.Awake();
        //     Duration = _duration;
        //     // _shaderMaterial.SetFloat("_scale", 0);
        // }
        protected override void Evaluate(float t)
        {
            // Center the bone


            // Scale the bone
            float scaleT = Easing.CalcLinear(t);
            float scale = scaleT * _maxScale;
            // Spin the bone
            // float angleT = Easing.CalcLinear(t);
            // float angle = angleT * 360f;
            _currentScale = scale;
            _shaderMaterial.SetFloat("_scale", scale);
            // _shaderMaterial.SetFloat("_angle", angle);
        }
    }
}