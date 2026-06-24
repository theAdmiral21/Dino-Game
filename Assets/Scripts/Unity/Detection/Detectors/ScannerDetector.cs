// using Enemy.Core.Detectors;
// using Enemy.Application.DataStructures;
// using Enemy.Core.Detectors.Abstractions;
// using UnityEngine;
// using Primitives.Detectors;
// using Unity.Common.Unity;
// using Game.Core.Effects;
// using UnityEngine.Rendering;
// using UnityEditor.Experimental.GraphView;
// using Movement.Core.Movement.DataStructures;
// using NPC.Core.Effects;

// namespace Enemy.Unity.Detectors
// {
//     /// <summary>
//     /// Class that gives enemies the ability to chase after a player if they have line of sight.
//     /// </summary>
//     public class ScannerDetector : MonoBehaviour, IPlayerDetector
//     {
//         // [SerializeField] private SerializedInterface<IEffectPlayer> _scanEffectMono;
//         // private IEffectPlayer _scanEffect => _scanEffectMono.Interface;
//         [SerializeField] private LayerMask _obstacleMask; // What blocks vision
//         [SerializeField] private float _detectionRange = 8f;
//         // How long after losing line of sight until the enemy gives up
//         [SerializeField] private float _interestTimer = 3f;
//         // Where enemy "looks" from (their eyes)
//         [SerializeField] private Transform _visionOrigin;
//         [SerializeField] private Transform _parentTransform;

//         [SerializeField] private SerializedInterface<IEffectPlayer> _scannerEffectMono;
//         private IEffectPlayer _scannerEffect => _scannerEffectMono.Interface;

//         [Header("Scan and Sweep Parameters")]
//         public float ScanDist = 5f;
//         public float SweepSpeed = 2f;
//         public float SweepRange = 60f;
//         public bool DrawDebug = false;

//         private Transform _playerTransform;
//         private Vector2 _lastKnown;
//         private bool _isStale = true;
//         private float _time;
//         private Vector3 _scanDir;
//         private float _facing => Mathf.Sign(_parentTransform.localScale.x);

//         public IDetectionData DetectPlayer()
//         {
//             // Perform a cast to see if we can see the player
//             if (_playerTransform == null)
//             {
//                 // This is probably a sign I need to rethink my effects since this effect result does nothing
//                 _scannerEffect.Play(new AlertEffect());
//                 MarkLastSeen();
//                 return Search();
//             }
//             else
//             {
//                 // _scannerEffect.Stop();
//                 return Track();
//             }
//         }

//         public void ClearData()
//         {
//             _isStale = true;
//             _playerTransform = null;

//         }

//         private IDetectionData Search()
//         {
//             DebugTracking();

//             // Sweep the scan to match the effect
//             _time += Time.deltaTime;

//             float angle = Mathf.Sin(_time * SweepSpeed) * (SweepRange * Mathf.Deg2Rad);
//             float x = Mathf.Cos(angle);
//             float y = Mathf.Sin(angle);

//             Vector3 dir = new Vector3(x, y, 0);
//             _scanDir = transform.TransformDirection(dir);

//             RaycastHit2D hit = Physics2D.Raycast(
//                                     _visionOrigin.position,
//                                     _scanDir,
//                                     _detectionRange * _facing,
//                                     _obstacleMask);

//             return InterpretHit(hit);
//         }

//         private IDetectionData InterpretHit(RaycastHit2D hit)
//         {

//             // Check if we saw the player
//             if (hit.collider != null)
//             {
//                 var dist = Vector2.Distance(_visionOrigin.position, hit.point);
//                 if (hit.collider.CompareTag("Player"))
//                 {
//                     // Found player
//                     _playerTransform = hit.transform;
//                     return ReturnObjectData();
//                 }
//                 _playerTransform = null;
//                 if (DrawDebug) Debug.DrawRay(_visionOrigin.position, _facing * _scanDir * dist, Color.red);
//                 return ReturnLastKnown();
//             }
//             _playerTransform = null;
//             if (DrawDebug) Debug.DrawRay(_visionOrigin.position, _facing * _scanDir * _detectionRange, Color.red);
//             return ReturnLastKnown();
//         }

//         private IDetectionData Track()
//         {
//             DebugTracking();
//             var dir = _playerTransform.position - _visionOrigin.position;
//             var dist = Vector2.Distance(_visionOrigin.position, _playerTransform.position);
//             RaycastHit2D hit = Physics2D.Raycast(
//                                     _visionOrigin.position,
//                                     dir,
//                                     dist,
//                                     _obstacleMask);
//             // Check if we saw the player
//             if (hit.collider != null)
//             {
//                 if (hit.collider.CompareTag("Player"))
//                 {
//                     _playerTransform = hit.transform;
//                     _lastKnown = _playerTransform.position;
//                     _isStale = false;
//                     return ReturnObjectData();
//                 }
//             }
//             _playerTransform = null;
//             return ReturnLastKnown();
//         }

//         private void MarkLastSeen()
//         {
//             if (_lastKnown == Vector2.zero) return;
//             var dir = _lastKnown - (Vector2)_visionOrigin.position;
//             var dist = Vector2.Distance(_visionOrigin.position, _lastKnown);
//             if (DrawDebug) Debug.DrawRay(_visionOrigin.position, dir * dist, Color.yellow);
//         }

//         private void DebugTracking()
//         {
//             if (_playerTransform != null)
//             {
//                 var dir = _playerTransform.position - _visionOrigin.position;
//                 var dist = Vector2.Distance(_visionOrigin.position, _playerTransform.position);

//                 if (DrawDebug) Debug.DrawRay(_visionOrigin.position, dir * dist, Color.green);
//             }
//         }

//         private void OnDrawGizmosSelected()
//         {
//             if (_visionOrigin == null) return;

//             // Draw detection range
//             Gizmos.color = Color.yellow;
//             Vector3 dir = new Vector3(_parentTransform.localScale.x, 0, 0);
//             Gizmos.DrawRay(_visionOrigin.position, dir);
//         }

//         private IDetectionData ReturnObjectData()
//         {
//             return new LineOfSiteData(DetectionReading.Current, _playerTransform.position, _visionOrigin.position);
//         }

//         private IDetectionData ReturnLastKnown()
//         {
//             if (_lastKnown != null && !_isStale)
//             {
//                 return new LineOfSiteData(DetectionReading.LastKnown, _lastKnown, _visionOrigin.position);
//             }
//             return null;
//         }
//     }
// }