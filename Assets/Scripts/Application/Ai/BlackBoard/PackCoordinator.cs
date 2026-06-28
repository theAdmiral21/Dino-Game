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
            for (int i = 0; i < PackMembers.Count; i++)
            {
                var member = PackMembers[i];
                if (member.Status.CurrentStatus == Status.Attacking)
                {
                    // Only add fresh data
                    Data.LastKnownLocation = new Observation<Vector2>
                    {
                        Data = member.Status.Perception.TargetPosition != null ? member.Status.Perception.TargetPosition.Value : Data.LastKnownLocation.Data,
                        TimeOfObservation = Time.time,
                    };
                }
            }
        }
        public void Triangulate()
        {
            Debug.Log($"Triangulating the player's location!");
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