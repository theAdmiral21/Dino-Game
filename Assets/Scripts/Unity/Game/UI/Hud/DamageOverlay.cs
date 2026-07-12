using System;
using Core.Common.Abstractions;
using Core.Game.UI.Menus.Hud;
using Game.Core.Health;
using Primitives.EventBus.Abstractions;
using Primitives.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Game.UI.Hud
{
    public class DamageOverlay : MonoBehaviour, IDamageOverlay
    {
        [SerializeField] protected Image _image;
        [SerializeField] protected Material _shaderMaterial;
        private readonly static int _intensityHash = Shader.PropertyToID("_damageIntensity");
        private void Awake()
        {
            _shaderMaterial = new Material(_shaderMaterial); // create instance from asset
            _image.material = _shaderMaterial; // assign instance to renderer
        }

        public void UpdateOverlay(HealthState state)
        {
            // get the health state, note this fires AFTER health is updated
            switch (state)
            {
                case HealthState.Fine:
                    {
                        _shaderMaterial.SetFloat(_intensityHash, 1f);
                        break;
                    }
                case HealthState.Wounded:
                    {
                        _shaderMaterial.SetFloat(_intensityHash, .5f);
                        break;
                    }
                case HealthState.Injured:
                    {
                        _shaderMaterial.SetFloat(_intensityHash, .25f);
                        break;
                    }
                case HealthState.CriticallyInjured:
                    {
                        _shaderMaterial.SetFloat(_intensityHash, .125f);
                        break;
                    }
                case HealthState.Dead:
                    {
                        _shaderMaterial.SetFloat(_intensityHash, .01f);
                        break;
                    }
            }
        }
    }
}