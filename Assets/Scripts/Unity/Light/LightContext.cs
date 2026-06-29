using System.Collections.Generic;
using Core.Light;
using UnityEngine;

namespace Unity.Light
{
    public class LightContext : MonoBehaviour, ILightContext
    {
        [SerializeField] private LayerMask _lightLayer;
        private ContactFilter2D _filter;
        private ILightVolume _lightVolume;
        private void Awake()
        {
            if (_lightLayer.value == 0)
            {
                _lightLayer = LayerMask.GetMask("Light");
            }
            _filter = new ContactFilter2D();
            _filter.SetLayerMask(_lightLayer);
            _filter.useLayerMask = true;
        }
        public LightData GetAmbientLight()
        {
            // ILightVolume volume = GetLightVolume();
            // if (volume == null)
            // {
            //     return new LightData(0, null);
            // }
            float ambientLight = CalcAmbientLight(_lightVolume);
            return new LightData(ambientLight, _lightVolume);

        }
        private void FixedUpdate()
        {
            _lightVolume = GetLightVolume();
        }

        private float CalcFallOff(ILightVolume volume)
        {
            Debug.Assert(volume != null, $"Light volume is null");
            Debug.Assert(volume.SourcePosition != null, $"Light volume.SourcePosition is null");
            float distance = Vector2.Distance(volume.SourcePosition, transform.position);
            float fallOff = 1 - (distance / volume.Range);
            return fallOff;
        }
        private ILightVolume GetLightVolume()
        {
            List<Collider2D> colliders = new();
            ILightVolume lightVolume = null;
            float maxAmbient = 0;

            int total = Physics2D.OverlapCircle(transform.position, 1f, _filter, colliders);

            if (total == 0) Debug.LogError($"{gameObject.name} failed to find any light sources");

            for (int i = 0; i < total; i++)
            {
                if (colliders[i] == null) continue;

                colliders[i].TryGetComponent<ILightVolume>(out var volume);
                if (volume != null)
                {
                    float ambient = CalcAmbientLight(volume);
                    if (ambient > maxAmbient)
                    {
                        maxAmbient = ambient;
                        lightVolume = volume;
                    }
                }
            }
            return lightVolume;
        }

        private float CalcAmbientLight(ILightVolume volume)
        {
            float fallOff = CalcFallOff(volume);
            float ambientLight = fallOff * volume.Intensity;
            return ambientLight;
        }
    }
}