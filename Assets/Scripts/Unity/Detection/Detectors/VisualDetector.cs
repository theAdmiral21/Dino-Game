using System.Collections.Generic;
using Core.Detection.Visual;
using Core.Detection.Visual.DataStructures;
using Core.Light;
using Primitives.Health;
using Unity.Common.Unity;
using Unity.Detection.Detectors.DataStructures;
using UnityEditor;
using UnityEngine;

namespace Unity.Detection.Detectors
{
    public class VisualDetector : MonoBehaviour, IVisualDetector
    {
        [SerializeField] private LayerMask _obstacleMask; // What blocks vision
        [SerializeField] private DetectorStatsSO _statsSO;

        [SerializeField] private SerializedInterface<ILightContext> _lightContextMono;
        private ILightContext _lightContext => _lightContextMono.Interface;

        public float VisualDistance { get; private set; }

        public float Acuity { get; private set; }

        public float NightVision { get; private set; }

        public float AmbientLight { get; private set; }

        public float FOV = 30f;
        public float RaycastCount = 5;

        [SerializeField] private Transform _parentTransform;

        [Header("Debug")]
        [SerializeField] private bool _drawDebug;

        private float _facing => Mathf.Sign(_parentTransform.localScale.x);

        private bool _isTracking;

        // Visual Data returned from target
        private Vector2 _targetLastKnown;
        private Vector2 _targetFacing;
        private Vector2 _targetVelocity;
        private HealthState _targetHealthState;

        private void Awake()
        {
            var temp = _statsSO.BuildRunTime();
            Acuity = temp.VisualAcuity;
            VisualDistance = temp.SightDistance;
            NightVision = temp.NightVision;
        }

        private void FixedUpdate()
        {
            Look();
        }
        public VisualData? Search()
        {
            if (!_isTracking)
            {
                return Look();
            }
            else
            {
                return Track();
            }
        }
        public VisualData? Look()
        {
            // Scan the area from origin a set distance using a ray cast
            List<RaycastHit2D> hits = Scan();
            // Debug.Log($"Got {hits.Count} hits");
            for (int i = 0; i < hits.Count; i++)
            {
                // Debug.Log($"Checking {hits[i].collider.name}");
                if (hits[i].collider.TryGetComponent<ILightContext>(out var lightContext))
                {
                    // Debug.Log($"Found light context: {lightContext != null}");

                    LightData targetLightData = lightContext.GetAmbientLight();
                    // Debug.Log($"target light data value: {targetLightData.AmbientLight}; source: {targetLightData.LightSource}");
                    if (targetLightData.LightSource == null) continue;

                    float perceived = CalcVisualScore(hits[i], targetLightData);

                    if (_drawDebug)
                    {
                        Debug.Log($"Detected {hits[i].collider.name}; VisualScore: {perceived}");
                    }
                    if (perceived > Acuity)
                    {
                        _targetLastKnown = hits[i].collider.transform.position;
                        _isTracking = true;
                        return Track();
                    }
                }
            }
            return null;
        }

        public VisualData? Track()
        {
            // Maintain a visual lock on the target and start providing data
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                _targetLastKnown.normalized,
                                                VisualDistance,
                                                _obstacleMask);

            if (hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    _targetLastKnown = hit.transform.position;
                    float targetDistance = Vector2.Distance(transform.position, _targetLastKnown);
                    // _targetFacing = hit.transform
                    // _targetVelocity
                    // _targetHealthState
                    Debug.LogError($"Implement getting the rest of this data from the player!");

                    return new VisualData
                    {
                        DetectionTime = Time.fixedTime,
                        DistanceFraction = targetDistance / VisualDistance,
                        TargetPosition = _targetLastKnown,
                    };
                }
            }
            return null;
        }

        private float CalcVisualScore(RaycastHit2D hit, LightData targetLightData)
        {
            // How visible is the target objectively?
            // AmbientLight already accounts for distance from light source via falloff
            float lighting = targetLightData.AmbientLight;

            // Normalize distance — 0 means at max range, 1 means right next to observer
            float normalizedDistance = 1f - Mathf.Clamp01(hit.distance / VisualDistance);

            // Movement contribution — you don't have this yet, placeholder 0
            float movement = 0f;

            // Base visibility — lighting * how close they are
            float baseVisibility = lighting * normalizedDistance;

            // Movement scales with how lit the target is
            float movementContribution = movement * Mathf.Lerp(0f, 0.4f, lighting);

            // Raw target visibility
            float targetVisibility = Mathf.Clamp01(baseVisibility + movementContribution);

            // Observer perception — how well does THIS detector perceive that visibility?
            // NightVision inverts the lighting relationship — better in darkness
            float darkness = 1f - lighting;
            float effectiveNightVision = NightVision * darkness;
            float perceived = targetVisibility * Acuity * (1f + effectiveNightVision);

            return Mathf.Clamp01(perceived);

        }

        private List<RaycastHit2D> Scan()
        {
            List<RaycastHit2D> hits = new();
            for (int i = 0; i < RaycastCount; i++)
            {
                float angle = i * (FOV / RaycastCount) - (FOV / 2);
                angle *= Mathf.Deg2Rad;
                // Debug.Log($"angle: {angle * Mathf.Rad2Deg}");
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);
                Vector3 dir = new Vector3(x, y, 0);
                Vector3 scanDir = transform.TransformDirection(dir);

                RaycastHit2D hit = Physics2D.Raycast(
                        transform.position,
                        scanDir,
                        VisualDistance * _facing,
                        _obstacleMask);

                // Dinos only care about the player... except for the rex. She likes flares.
                // Debug.Log($"Hit is null: {hit}; Angle{angle * Mathf.Rad2Deg}");
                if (hit && hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    hits.Add(hit);
                }

                if (_drawDebug)
                {
                    Debug.DrawRay(transform.position, scanDir * hit.distance * _facing, Color.red);
                }
            }
            return hits;
        }
    }
}