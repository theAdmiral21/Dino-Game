using System;
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

        // Data classes for this frame
        VisualData? _visualData;
        AudioData? _audioData;
        OlfactoryData? _scentData;
        public DetectorBrain(IVisualDetector visualDetector)
        {
            _visualDetector = visualDetector;
        }
        public void Tick(float dt)
        {

            _visualData = _visualDetector.Look();

        }
        public PerceptionState DrawConclusions()
        {
            return new PerceptionState
            {
                ConfidenceLevel = CalcConfidence(),
                // TargetPosition = _visualData.
            };
        }

        public void OnAudioEvent(AudioData data)
        {
            _audioData = data;
        }

        public void OnScentEvent(OlfactoryData data)
        {
            _scentData = data;
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
    }
}