using System.Collections;
using Core.Environment.Ambience;
using Core.Game.Events.Abstractions;
using Game.Unity.Events;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Unity.Environment.Ambience.Lights
{
    public class FluorescentLight : MonoBehaviour, IAmbienceEffect
    {
        [SerializeField] private AudioFeedBack _startUpClicking;
        [SerializeField] private AudioFeedBack _hum;
        [SerializeField] private Light2D _light;
        [SerializeField] private float _startUpTime;


        private float _targetIntensity = 1;
        private float _startIntensity = 0;

        [ContextMenu("Test Startup")]
        private void TestStartUp()
        {
            Perform();
        }

        public void Perform()
        {
            StartCoroutine(StartUpRoutine());
        }

        private IEnumerator StartUpRoutine()
        {
            // start the flicker noise
            _startUpClicking.React();
            StartCoroutine(FlickerLights(5, .05f, .2f, .1f, .4f));
            // yield return new WaitForSeconds(_startUpTime * .25f);
            // ramp the intensity
            StartCoroutine(RampIntensity(_startUpTime));
            _hum.React();

            // turn the light on and off
            yield return new WaitForSeconds(_startUpTime);
        }
        private IEnumerator RampIntensity(float duration)
        {
            float elapsed = 0f;
            while (elapsed < _startUpTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _startUpTime);
                _light.intensity = Mathf.Lerp(_startIntensity, _targetIntensity, t);
                yield return null;
            }
            _light.intensity = 1;
        }

        private IEnumerator FlickerLights(int count, float minOn, float maxOn, float minOff, float maxOff)
        {
            for (int i = 0; i < count; i++)
            {
                _light.enabled = true;
                yield return new WaitForSeconds(Random.Range(minOn, maxOn));

                _light.enabled = false;
                yield return new WaitForSeconds(Random.Range(minOff, maxOff));
            }
            _light.enabled = true;
        }
    }
}