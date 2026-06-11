using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace PlayerController.Unity.Effects
{
    public class PlayerLightController : MonoBehaviour
    {
        [SerializeField] private Light2D _playerLight;
        private Coroutine _flashCoroutine;
        public void EnterZoomiesMode()
        {
            if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
            _flashCoroutine = StartCoroutine(FlashRoutine());

            // NOTE This coroutine does funky stuff if you die while its running...

            // SetVolume(5);
            // SetFallOff(.5f);
            // SetOuterRadius(8);
            // SetIntensity(5);
        }

        public void UpdateZoomiesMode()
        {
            SetIntensity(3);
            SetVolume(1);
            SetFallOff(0.5f);
            SetOuterRadius(1.3f);
        }

        public void ExitZoomiesMode()
        {
            SetIntensity(1);
            SetVolume(0);
            SetFallOff(1f);
            SetOuterRadius(1.3f);
        }

        private void SetIntensity(float val)
        {
            _playerLight.intensity = val;
        }
        private void SetVolume(float val)
        {
            _playerLight.volumeIntensity = val;
        }
        private void SetFallOff(float val)
        {
            _playerLight.falloffIntensity = val;
        }
        private void SetOuterRadius(float val)
        {
            _playerLight.pointLightOuterRadius = val;
        }

        private IEnumerator FlashRoutine()
        {
            float elapsed = 0f;
            float duration = .1f;

            // Ramp up
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float lerpVal = Mathf.Lerp(0f, 1f, elapsed / duration);
                // Final values
                _playerLight.intensity = lerpVal * 1f;
                _playerLight.pointLightOuterRadius = lerpVal * 8;
                _playerLight.falloffIntensity = lerpVal * 0.5f;
                _playerLight.volumeIntensity = lerpVal * 2;
                yield return null;
            }

            elapsed = 0f;

            // Ramp down
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float lerpVal = Mathf.Lerp(1f, 0f, elapsed / duration);
                // Final values
                _playerLight.intensity = lerpVal * 1f;
                _playerLight.pointLightOuterRadius = lerpVal * 3f;
                _playerLight.falloffIntensity = lerpVal * 0.5f;
                _playerLight.volumeIntensity = lerpVal * 1.5f;
                yield return null;
            }
        }
    }
}