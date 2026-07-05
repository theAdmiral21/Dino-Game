using System;
using System.Collections.Generic;
using Core.Detection.Olfactory;
using Core.Detection.Olfactory.DataStructures;
using Core.Detection.Services;
using Game.Core.Execution;
using Infrastructure.Unity.Registries;
using Unity.Detection.Detectors.DataStructures;
using UnityEngine;

namespace Unity.AI.Detection.Detectors
{
    public class ScentDetector : SelfRegister<IInitializable<IGameContext>>, IScentDetector, IInitializable<IGameContext>
    {
        [SerializeField] private DetectorStatsSO _statsSO;
        public float Sensitivity => _sensitivity;
        private float _sensitivity;
        private IScentSampleService _scentSampler;

        public event Action<OlfactoryData> ScentEvent;

        [SerializeField] private int _priority = 0;
        public int Priority => _priority;
        private void Awake()
        {
            base.Awake();

            var temp = _statsSO.BuildRunTime();
            _sensitivity = temp.AudioAcuity;
        }
        public void Initialize(IGameContext context)
        {
            _scentSampler = context.DetectionServices.ScentSampleService;
        }

        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_scentSampler != null, $"Unable to set the scent sampler");
        }
        public void Scent()
        {
            // When asked, sample the area for a scent
            List<OlfactoryData> data = _scentSampler.Sample(transform.position, _sensitivity);

            // sample the scents return the closest data point
            float dist = Mathf.Infinity;
            int ndx = -1;
            for (int i = 0; i < data.Count; i++)
            {
                float scentDist = Vector2.Distance(transform.position, data[i].ScentPosition);
                if (scentDist < dist)
                {
                    ndx = i;
                    dist = scentDist;
                }
            }
            ScentEvent?.Invoke(data[ndx]);
        }


    }
}