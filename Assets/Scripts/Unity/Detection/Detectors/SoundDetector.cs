using Core.Detection.Audio;
using Core.Detection.Audio.DataStructures;
using Unity.Detection.Detectors.DataStructures;
using UnityEngine;

namespace Unity.Detection.Detectors
{
    public class SoundDetector : MonoBehaviour, ISoundDetector, IInitSoundDetector
    {
        [SerializeField] private DetectorStatsSO _statsSO;
        public float Sensitivity => _sensitivity;
        private float _sensitivity;

        private void Awake()
        {
            var temp = _statsSO.BuildRunTime();
            _sensitivity = temp.AudioAcuity;
        }
        public void Init(float audioAcuity)
        {
            _sensitivity = audioAcuity;
        }
        public void Detect(EmittedSound sound)
        {
            // Calc fall off 
            float fallOff = CalcFallOff(sound);
            // Calc attenuation
            float dist = Vector2.Distance(sound.Origin, transform.position);
            float attenuation = CalcAttenuation(dist);
            // Calc perceived intensity
            float perceivedIntensity = fallOff * attenuation * _sensitivity;
            Debug.Log($"Perceived intensity: {perceivedIntensity}");
            // Threshold and react will be governed by a different class that handle behavior
            float threshold = 5f;
            if (perceivedIntensity > threshold)
            {
                // React();
                Debug.Log($"Reacting to sound!");
            }
        }

        private float CalcFallOff(EmittedSound sound)
        {
            float distance = Vector2.Distance(sound.Origin, transform.position);
            float fallOff = 1 - (distance / sound.Radius);
            return fallOff;
        }

        private float CalcAttenuation(float distance)
        {
            // some sort of occlusion magic
            return 1;
        }

    }
}