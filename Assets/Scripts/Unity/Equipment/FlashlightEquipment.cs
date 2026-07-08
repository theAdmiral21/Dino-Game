using System;
using Core.Equipment;
using Game.Unity.Events;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Unity.Equipment
{
    public class FlashlightEquipment : MonoBehaviour, IFlashlight
    {
        [Header("Required references")]
        [SerializeField] private Transform _facingTransform;
        [SerializeField] private Transform _rootTransform;
        private Vector3 _facingScale;
        [SerializeField] private Light2D _light;

        [Header("Optional Audio")]
        [SerializeField] private AudioFeedBack _lightOnAudio;
        [SerializeField] private AudioFeedBack _lightOffAudio;

        private float _defaultIntensity;
        public bool IsOn { get; private set; }
        public float DischargeRate { get; private set; }
        public float ChargeRate { get; private set; }

        private void Awake()
        {
            _defaultIntensity = _light.intensity;
        }

        public void ToggleFlashlight()
        {
            IsOn = !IsOn;
            SetLightIntensity();
        }

        private void SetLightIntensity()
        {
            if (IsOn)
            {
                if (_lightOnAudio != null) _lightOnAudio.React();
                _light.intensity = _defaultIntensity;
            }
            else
            {
                if (_lightOffAudio != null) _lightOffAudio.React();
                _light.intensity = 0;
            }
        }
        private void Update()
        {
            UpdateFacing();
        }
        private void UpdateFacing()
        {
            _facingScale.x = Mathf.Sign(_facingTransform.localScale.x);
            _rootTransform.localScale = _facingScale;
        }
    }
}