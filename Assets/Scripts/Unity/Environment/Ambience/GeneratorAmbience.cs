using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Core.Environment.Ambience;
using Game.Unity.Events;
using Unity.Hierarchy.Editor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Unity.Environment.Ambience
{
    public class GeneratorAmbience : MonoBehaviour, IAmbienceEffect
    {
        [SerializeField] private List<Light2D> _onLights;
        [SerializeField] private List<Light2D> _offLights;
        [SerializeField] private List<Light2D> _emergencyLights;
        [SerializeField] private float _startUpTime;

        private float _targetIntensity = 1;
        private float _startIntensity = 0;


        [SerializeField] private AudioFeedBack _switchClose;
        [SerializeField] private AudioFeedBack _startUpSound;
        [SerializeField] private AudioFeedBack _idleHum;

        [Header("Start up timing")]
        [SerializeField] private float _switchDelay;

        public void Perform()
        {
            StartCoroutine(StartUpRoutine());
        }

        private IEnumerator StartUpRoutine()
        {
            // toggle from red to green for each generator
            yield return StartCoroutine(LightSequence());
            // play a single start up sound
            yield return StartCoroutine(PlayStartSound());
            // play an idle sound
            _idleHum.React();
            // slowly turn on emergency lights
            yield return StartCoroutine(RampIntensity());
        }

        private IEnumerator LightSequence()
        {
            for (int i = 0; i < _onLights.Count; i++)
            {
                _onLights[i].enabled = true;
                _offLights[i].enabled = false;
                _switchClose.React();
                // wait for the close sound
                yield return new WaitForSeconds(_switchClose.ClipLength);
                // Delay between iterations
                yield return new WaitForSeconds(_switchDelay);
            }
        }

        private IEnumerator PlayStartSound()
        {
            _startUpSound.React();
            yield return new WaitForSeconds(_startUpSound.ClipLength);
        }

        private IEnumerator RampIntensity()
        {
            float elapsed = 0f;
            while (elapsed < _startUpTime)
            {
                elapsed += Time.deltaTime;
                for (int i = 0; i < _onLights.Count; i++)
                {
                    float t = Mathf.Clamp01(elapsed / _startUpTime);
                    _emergencyLights[i].intensity = Mathf.Lerp(_startIntensity, _targetIntensity, t);
                }
                yield return null;
            }
            for (int i = 0; i < _onLights.Count; i++)
            {
                _emergencyLights[i].intensity = 1;
            }
        }
    }
}