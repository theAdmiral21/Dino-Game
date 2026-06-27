using System.Collections.Generic;
using Core.Ai.BlackBoard.DataStructures;

namespace Core.Ai.BlackBoard
{
    public interface IPackCoordinator
    {
        public PackData Data { get; }
        public List<IPackMember> PackMembers { get; }
        public void EvaluateBlackBoard();
        public void Triangulate();
        public void AddMember(IPackMember member);
        public void RemoveMember(IPackMember member);
        public void UpdateMemberStatus();
        public void UpdateMemberBehavior(float dt);
    }
}