using System.Collections.Generic;
using System.Linq;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using Primitives.Health;
using Unity.Ai.BlackBoard;
using Unity.Tools.DrawingTools;
using UnityEngine;

namespace Unity.Tools.RaptorDebug
{
    public class PackStatusDebugger : MonoBehaviour
    {
        private List<IPackMember> _packMembers = new();
        [SerializeField] private List<MemberStatus> _debugMembers;

        [SerializeField] private PackManager _packManager;
        private PackData _packData => _packManager.PackData;
        // [SerializeField] private DebugPackData _debugPackData;

        [SerializeField] Vector2 _lastKnownDebug;
        [SerializeField] float _lastKnownTime;
        [SerializeField] Vector2 _bestGuessDebug;
        [SerializeField] float _bestGuessTime;
        [SerializeField] float _bestGuessConfidenceDebug;
        [SerializeField] Vector2 _facingDebug;
        [SerializeField] float _facingTimeDebug;
        [SerializeField] HealthState _healthDebug;
        [SerializeField] AlertLevel _alertDebug;
        [SerializeField] float _healthTimeDebug;


        private void Awake()
        {
            // Get the raptors
            IPackMember[] members = GetComponentsInChildren<IPackMember>();

            _packMembers = members.ToList();

            // Convert the members to the implementation?
            foreach (var member in _packMembers)
            {
                var concreteMember = member as PackMember;
                _debugMembers.Add(concreteMember.Status);
            }

            // _debugPackData = new();
        }

        private void LateUpdate()
        {
            if (_packData.LastKnownLocation.HasValue)
            {
                _lastKnownDebug = _packData.LastKnownLocation.Value.Data;
                _lastKnownTime = _packData.LastKnownLocation.Value.Age;

                DrawUtil.DrawDebugCircle(_lastKnownDebug, 1, Color.red);
            }

            if (_packData.BestGuessLocation.HasValue)
            {
                _bestGuessDebug = _packData.BestGuessLocation.Value.Data;
                _bestGuessTime = _packData.BestGuessLocation.Value.Age;
            }

            _bestGuessConfidenceDebug = _packData.BestGuessConfidence;

            if (_packData.TargetFacing.HasValue)
            {
                _facingDebug = _packData.TargetFacing.Value.Data;
                _facingTimeDebug = _packData.TargetFacing.Value.Age;
            }

            _healthDebug = _packData.TargetHealth.Data;
            _healthTimeDebug = _packData.TargetHealth.Age;
            _alertDebug = _packData.PackAlertLevel;
        }
    }
}