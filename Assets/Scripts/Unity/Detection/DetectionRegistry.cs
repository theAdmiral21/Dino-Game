using System.Collections.Generic;
using Core.Detection;
using Core.Detection.Audio;
using Infrastructure.Unity;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Unity.Detection
{
    public class DetectionRegistry : MonoBehaviour, IDetectionRegistry
    {
        public IReadOnlyCollection<ISoundDetector> SoundDetectors => _soundDetectors.Entities;
        private Registry<ISoundDetector> _soundDetectors = new();

        public IReadOnlyCollection<ISoundEmitter> SoundEmitters => _soundEmitters.Entities;
        private Registry<ISoundEmitter> _soundEmitters = new();

        public void Awake()
        {
            RegistryGateway.SetRegistry(_soundDetectors);
            RegistryGateway.SetRegistry(_soundEmitters);
        }
    }
}