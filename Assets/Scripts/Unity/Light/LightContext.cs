using System.Collections.Generic;
using Codice.Client.Common.GameUI;
using Core.Light;
using UnityEngine;

namespace Unity.Light
{
    public class LightContext : MonoBehaviour, ILightContext
    {
        [SerializeField] private LayerMask _lightLayer;
        private ContactFilter2D _filter;
        private void Awake()
        {
            if (_lightLayer.value == 0)
            {
                _lightLayer = LayerMask.GetMask("Light");
            }
            // Debug.Log($"Light layer mask value: {_lightLayer.value}");
            _filter = new ContactFilter2D();
            _filter.SetLayerMask(_lightLayer);
            _filter.useLayerMask = true;
        }
        public LightData GetAmbientLight()
        {
            ILightVolume volume = GetLightVolume();
            // Debug.Log($"Found light volume: {volume}");
            if (volume == null)
            {
                return new LightData(0, null);
            }
            float ambientLight = CalcAmbientLight(volume);
            return new LightData(ambientLight, volume);

        }
        private float CalcFallOff(ILightVolume volume)
        {
            float distance = Vector2.Distance(volume.SourcePosition, transform.position);
            float fallOff = 1 - (distance / volume.Range);
            return fallOff;
        }
        private ILightVolume GetLightVolume()
        {
            List<Collider2D> colliders = new();
            ILightVolume lightVolume = null;
            float maxAmbient = 0;

            // Temporarily query with no layer mask to find EVERYTHING
            // List<Collider2D> allColliders = new();
            // ContactFilter2D noFilter = new ContactFilter2D().NoFilter();
            // int allTotal = Physics2D.OverlapPoint(transform.position, noFilter, allColliders);
            // Debug.Log($"No filter found: {allTotal} colliders");
            // foreach (var col in allColliders)
            // {
            //     Debug.Log($"Found: {col.gameObject.name} on layer {col.gameObject.layer} ({LayerMask.LayerToName(col.gameObject.layer)})");
            // }

            // Debug.Log($"Overlap point at {transform.position}");
            // Debug.Log($"Filter layer mask: {_filter.layerMask.value}, useLayerMask: {_filter.useLayerMask}");
            // int total = Physics2D.OverlapPoint(transform.position, _filter, colliders);
            int total = Physics2D.OverlapCircle(transform.position, 1f, _filter, colliders);

            for (int i = 0; i < total; i++)
            {
                if (colliders[i] == null) continue;

                colliders[i].TryGetComponent<ILightVolume>(out var volume);
                if (volume != null)
                {
                    float ambient = CalcAmbientLight(volume);
                    // Debug.Log($"{gameObject.name} found volume: {volume != null} with ambient light: {ambient}");
                    if (ambient > maxAmbient)
                    {
                        // Debug.Log($"Setting ambient to: {ambient} and light volume to: {volume}");
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