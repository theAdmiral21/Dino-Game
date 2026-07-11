namespace Core.Ai.BlackBoard
{
    public interface IPackManager : IPackDataProvider
    {
        public IPackCoordinator Coordinator { get; }
        public void AddMember(IPackMember member);
        public void RemoveMember(IPackMember member);
    }
}