using System.Collections.Generic;

namespace Core.Ai.BlackBoard
{
    public interface IPackManager
    {
        public IPackCoordinator Coordinator { get; }
        public void AddMember(IPackMember member);
        public void RemoveMember(IPackMember member);
    }
}