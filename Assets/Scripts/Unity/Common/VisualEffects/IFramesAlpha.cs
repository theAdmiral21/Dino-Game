using PlayerController.Core.Effects.DataStructures;
using UnityEngine;

namespace Gameplay.Common.Unity.VisualEffects
{
    public class IFramesAlpha : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        private Material _shaderMaterial;

        [Header("Flash cycle duration")]
        public float FlashFrequency;

        private bool _isActive;

        private void Awake()
        {
            _shaderMaterial = _spriteRenderer.material;
            _isActive = false;
        }

        public void UpdateAlpha(IFrameEffect iFrameResult)
        {
            if (!_isActive && iFrameResult.Approved)
            {
                _isActive = true;
            }
            else if (_isActive && !iFrameResult.Approved)
            {
                _isActive = false;
            }
            _shaderMaterial.SetFloat("_IFramesActive", _isActive ? 1f : 0f);
        }
    }
}