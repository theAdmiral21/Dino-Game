using Application.Detection;
using Core.Ai.State.BehaviorContext;
using Core.Detection;
using Core.Detection.Audio;
using Core.Detection.Olfactory;
using Core.Detection.Visual;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.Detection
{
    public class DetectionOrchestrator : MonoBehaviour, IDetectorOrchestrator
    {
        [SerializeField] private SerializedInterface<IVisualDetector> _visualDetectorMono;
        private IVisualDetector _visualDetector => _visualDetectorMono.Interface;

        [SerializeField] private SerializedInterface<ISoundDetector> _soundDetectorMono;
        private ISoundDetector _soundDetector => _soundDetectorMono.Interface;

        [SerializeField] private SerializedInterface<IScentDetector> _scentDetectorMono;
        private IScentDetector _scentDetector => _scentDetectorMono.Interface;

        public IDetectorBrain Brain => _brain;
        private IDetectorBrain _brain;
        // private bool _contextSet = false;
        private IPerceptionContext _perceptionContext;

        private void Awake()
        {
            Debug.Assert(_visualDetector != null, $"No visual detector found");
            Debug.Assert(_soundDetector != null, $"No sound detector found");
            Debug.Assert(_scentDetector != null, $"No scent detector found");

            // Build the brain
            _brain = new DetectorBrain(_visualDetector);

            // Sub to the events
            _soundDetector.AudioEvent += _brain.OnAudioEvent;
            _scentDetector.ScentEvent += _brain.OnScentEvent;

            // Handle timing issues
            if (_perceptionContext != null)
            {
                _brain.SetPerceptionContext(_perceptionContext);
            }
        }

        public void InitBrain(IPerceptionContext context)
        {
            _perceptionContext = context;
            if (_brain != null)
            {
                _brain.SetPerceptionContext(_perceptionContext);
                // _contextSet = true;
            }
        }
        private void FixedUpdate()
        {
            // if (_contextSet)
            // {
            _brain.Tick(Time.fixedDeltaTime);
            // }
        }

    }
}