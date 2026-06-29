using System.Collections.Generic;
using Application.Ai.BlackBoard;
using Core.Ai.BlackBoard;
using Core.Ai.BlackBoard.DataStructures;
using UnityEngine;

namespace Unity.Ai.BlackBoard
{
    public class PackManager : MonoBehaviour, IPackManager, IPackDataProvider
    {
        public IPackCoordinator Coordinator { get; private set; }

        public PackData PackData => Coordinator.Data;

        private void Awake()
        {
            // Build the coordinator
            Coordinator = new PackCoordinator();
            // Get all of the pack members
            IPackMember[] members = GetComponentsInChildren<IPackMember>();
            if (members == null) Debug.LogError($"No pack members were added as children to the pack manager. Check your hierarchy!");

            for (int i = 0; i < members.Length; i++)
            {
                AddMember(members[i]);

                // Eventually when you have a death event, sub to that here too!
            }
        }

        public void AddMember(IPackMember member)
        {
            Coordinator.AddMember(member);
        }

        public void RemoveMember(IPackMember member)
        {
            Coordinator.RemoveMember(member);
        }

        private void FixedUpdate()
        {
            // Update the raptors
            Coordinator.UpdateMemberStatus();
            // Update the black board
            Coordinator.EvaluateBlackBoard();
            // triangulate the location
            Coordinator.Triangulate();
            // Update the behavior
            Coordinator.UpdateMemberBehavior(Time.fixedDeltaTime);
        }
    }
}