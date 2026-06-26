using System;
using Core.Ai.State.BehaviorContext;
using Core.Detection;
using Core.Detection.Audio.DataStructures;
using Core.Detection.DataStructures;
using Core.Detection.Olfactory.DataStructures;
using Core.Detection.Visual;
using Core.Detection.Visual.DataStructures;
using UnityEngine;

namespace Application.Detection
{
    public class DetectorBrain : IDetectorBrain
    {
        private float _audioWeight = 1f;
        private float _scentWeight = 1f;

        private IVisualDetector _visualDetector;
        private IPerceptionContext _perceptionContext;

        // Interest timers, this eventually needs to be scriptable object
        private float _audioInterestTime = 5f;
        private float _audioInterestCounter;

        private float _scentInterestTime = 5f;
        private float _scentInterestCounter;

        // Data classes for this frame
        VisualData? _visualData;
        AudioData? _audioData;
        OlfactoryData? _scentData;

        public DetectorBrain(IVisualDetector visualDetector)
        {
            _visualDetector = visualDetector;
        }
        public void SetPerceptionContext(IPerceptionContext context)
        {
            _perceptionContext = context;
        }
        public void Tick(float dt)
        {

            _visualData = _visualDetector.Look();
            _perceptionContext.UpdatePerception(DrawConclusions());
            TickTimers(dt);

        }
        public PerceptionState DrawConclusions()
        {
            Debug.Log($"Drawing conclusions!");
            return new PerceptionState
            {
                // How confident is the brain in what is has perceived?
                ConfidenceLevel = CalcConfidence(),

                // Visual data


                // Audio data
                AudioDirection = _audioData.HasValue ? _audioData?.SoundDirection : null,
                TimeOfAudio = _audioData.HasValue ? _audioData.Value.DetectionTime : 0,
                AudioIntensity = _audioData.HasValue ? _audioData.Value.SoundIntensity : 0,
            };
        }

        public void OnAudioEvent(AudioData data)
        {
            _audioData = data;
            // This is where we would decide if the audio event was interesting or not, for now everything is interesting.
            Debug.Log($"Remember to gate audio interest in the future");
            SetAudioInterestTimer();
        }

        public void OnScentEvent(OlfactoryData data)
        {
            _scentData = data;
            Debug.Log($"Remember to gate scent interest in the future");
            SetScentInterestTimer();
        }

        private float CalcConfidence()
        {
            // Sight is a hard confirmation of the data
            if (_visualData.HasValue)
            {
                float rangeFactor = 1f - Mathf.Clamp01(_visualData.Value.DistanceFraction);
                return Mathf.Lerp(0.7f, 1.0f, rangeFactor);
            }

            float confidence = 0f;
            float totalWeight = 0f;

            if (_audioData.HasValue)
            {
                float audioContribution = _audioData.Value.SoundIntensity * _audioWeight;
                // SoundType mismatch could SUBTRACT confidence here
                confidence += audioContribution;
                totalWeight += _audioWeight;
            }

            if (_scentData.HasValue)
            {
                // Decayed scent = less confidence
                float scentContrib = _scentData.Value.ScentIntensity * +_scentWeight;
                confidence += scentContrib;
                totalWeight += +_scentWeight;
            }

            return totalWeight > 0f ? confidence / totalWeight : 0f;
        }

        private void SetAudioInterestTimer()
        {
            _audioInterestCounter = _audioInterestTime;
        }

        private void SetScentInterestTimer()
        {
            _scentInterestCounter = _scentInterestTime;
        }

        private void TickTimers(float dt)
        {
            if (_audioInterestCounter > 0)
            {
                _audioInterestCounter -= dt;
                Debug.Log($"Ticking audio interest timer");
            }
            else
            {
                if (_audioData != null)
                {
                    _audioData = null;
                }
            }

            if (_scentInterestCounter > 0)
            {
                _scentInterestCounter -= dt;
            }
            else
            {
                if (_scentData != null)
                {
                    _scentData = null;
                }
            }
        }

    }
}