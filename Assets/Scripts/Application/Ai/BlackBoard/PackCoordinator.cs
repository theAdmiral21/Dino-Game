using System.Collections.Generic;
using System.Linq;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using UnityEngine;

namespace Application.Ai.BlackBoard
{
    public class PackCoordinator : IPackCoordinator
    {
        public PackData Data { get; private set; } = new();

        public List<IPackMember> PackMembers => Data.Members.Values.ToList();

        private float _lostTrailTime = 15f;

        public void AddMember(IPackMember member)
        {
            if (!Data.Members.TryAdd(member.MemberId, member))
            {
                Debug.LogError($"Failed to add member {member.MemberId} to the pack.");
            }
        }
        public void RemoveMember(IPackMember member)
        {
            if (!Data.Members.Remove(member.MemberId))
            {
                Debug.LogError($"Failed to remove member {member.MemberId} from the pack.");
            }
        }

        public void EvaluateBlackBoard()
        {
            bool anyAttacking = PackMembers.Any(m => m.Status.CurrentStatus == Status.Attacking);

            if (anyAttacking)
            {
                // Escalate the whole pack
                Data.PackAlertLevel = AlertLevel.Engrossed;
            }

            // Clear stale data so the pack can genuinely lose the trail
            if (Data.LastKnownLocation.HasValue &&
                Data.LastKnownLocation.Value.IsStale(_lostTrailTime) &&
                !anyAttacking)
            {
                Data.LastKnownLocation = null;
            }

            if (PackMembers.Count > 0)
            {
                // Aggregate confidence
                Data.BestGuessConfidence = PackMembers.Max(m => m.Status.Perception.ConfidenceLevel);
            }
        }
        public void Triangulate()
        {
            Debug.Log($"Implement triangulating the player's location!");
        }

        public void UpdateMemberStatus()
        {
            for (int i = 0; i < PackMembers.Count; i++)
            {
                PackMembers[i].UpdateMemberStatus();
            }
        }

        public void UpdateMemberBehavior(float dt)
        {
            for (int i = 0; i < PackMembers.Count; i++)
            {
                PackMembers[i].RaptorController.TickBehaviorTree(dt);
            }
        }
    }
}