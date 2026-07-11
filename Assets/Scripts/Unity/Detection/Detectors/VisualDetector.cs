using System.Collections.Generic;
using Core.Detection.Visual;
using Core.Detection.Visual.DataStructures;
using Core.Light;
using Unity.Common.Unity;
using Unity.Detection.Detectors.DataStructures;
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
        // private float _targetFacing;
        // private Vector2 _targetVelocity;
        // private HealthState _targetHealthState;

        private void Awake()
        {
            var temp = _statsSO.BuildRunTime();
            Acuity = temp.VisualAcuity;
            VisualDistance = temp.SightDistance;
            NightVision = temp.NightVision;
        }

        private void FixedUpdate()
        {
            Search();
        }
        public VisualData? Search()
        {
            VisualData? res;
            if (!_isTracking)
            {
                res = Look();
            }
            else
            {
                res = Track();
            }
            Debug.Log($"Visual Data was null: {res == null}");
            return res;
        }
        public VisualData? Look()
        {
            // Scan the area from origin a set distance using a ray cast
            List<RaycastHit2D> hits = Scan();
            // Debug.Log($"Got {hits.Count} hits");
            for (int i = 0; i < hits.Count; i++)
            {
                bool hasLightContext = hits[i].collider.TryGetComponent<ILightContext>(out var lc);
                // Debug.Log($"Hit: {hits[i].collider.name} | HasLightContext: {hasLightContext} | LightSource null: {lc?.GetAmbientLight().LightSource == null}");

                // Debug.Log($"Checking {hits[i].collider.name}");
                // if (hits[i].collider.TryGetComponent<ILightContext>(out var lightContext))
                // {
                //     // Debug.Log($"Found light context: {lightContext != null}");

                //     LightData targetLightData = lightContext.GetAmbientLight();
                //     // Debug.Log($"target light data value: {targetLightData.AmbientLight}; source: {targetLightData.LightSource}");
                //     if (targetLightData.LightSource == null) continue;

                //     float perceived = CalcVisualScore(hits[i], targetLightData);

                //     if (_drawDebug)
                //     {
                //         Debug.Log($"Detected {hits[i].collider.name}; VisualScore: {perceived}");
                //     }

                float yOffset = hits[i].collider.bounds.extents.y;
                _targetLastKnown = hits[i].collider.transform.position + new Vector3(0, yOffset, 0);
                _isTracking = true;
                return Track();
                // }
            }
            return null;
        }

        public VisualData? Track()
        {
            // Maintain a visual lock on the target and start providing data
            Vector2 dir = (_targetLastKnown - (Vector2)transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                dir,
                                                VisualDistance,
                                                _obstacleMask);

            if (_drawDebug)
            {
                Debug.DrawRay(transform.position, dir * hit.distance, Color.green);
            }

            if (hit)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.TryGetComponent<IVisualDataProvider>(out var visualDataProvider);

                    if (visualDataProvider == null) return null;

                    _targetLastKnown = hit.transform.position;
                    float targetDistance = Vector2.Distance(transform.position, _targetLastKnown);

                    return new VisualData
                    {
                        DetectionTime = Time.fixedTime,
                        DistanceFraction = targetDistance / VisualDistance,
                        TargetPosition = _targetLastKnown,
                        TargetFacing = visualDataProvider.Facing,
                        TargetVelocity = visualDataProvider.Velocity,
                        Health = visualDataProvider.Health,
                    };
                }
            }
            _isTracking = false;
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
                float x = Mathf.Cos(angle);
                float y = Mathf.Sin(angle);
                Vector3 dir = new Vector3(x * _facing, y, 0);
                Vector3 scanDir = transform.TransformDirection(dir);

                RaycastHit2D hit = Physics2D.Raycast(
                        transform.position,
                        scanDir,
                        VisualDistance,
                        _obstacleMask);

                // Dinos only care about the player... except for the rex. She likes flares.
                // Debug.Log($"Hit is null: {hit}; Angle{angle * Mathf.Rad2Deg}");
                if (hit && hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    hits.Add(hit);
                }

                if (_drawDebug)
                {
                    Debug.DrawRay(transform.position, scanDir * hit.distance, Color.red);
                }
            }
            return hits;
        }
    }
}