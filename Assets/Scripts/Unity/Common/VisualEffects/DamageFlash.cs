using PlayerController.Core.Effects.DataStructures;
using UnityEngine;

namespace Gameplay.Common.Unity.VisualEffects
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        private Material _shaderMaterial;

        [Header("Flash cycle duration")]
        public float FlashTime;
        private float _flashCounter;
        private float _flashVal;
        private bool _isActive;

        private void Awake()
        {
            _shaderMaterial = _spriteRenderer.material;
        }

        public void UpdateFlash(DamageEffect damageResult)
        {
            if (damageResult.RemainingTime > 0)
            {
                _flashCounter -= damageResult.Dt;
                if (_flashCounter <= 0)
                {
                    _flashVal = (_flashVal + 1) % 2;
                    _flashCounter = FlashTime; // reset for next cycle
                }

                // Ensure we start flashing immediately on first call
                if (!_isActive)
                {
                    _flashVal = 1;
                    _flashCounter = FlashTime;
                    _isActive = true;
                    Debug.Log($"Flash started - Frame {Time.frameCount}");
                }
            }
            else
            {
                _flashVal = 0;
                _flashCounter = 0;
                _isActive = false;
            }

            _shaderMaterial.SetFloat("_FlashOpacity", _flashVal);
            Debug.Log($"Flash value: {_flashVal} - Frame {Time.frameCount}");
        }
    }
}