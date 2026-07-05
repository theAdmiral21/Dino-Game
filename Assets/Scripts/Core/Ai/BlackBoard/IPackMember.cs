namespace Core.Ai.BlackBoard
{
    public interface IPackMember
    {
        public IPackManager PackManager { get; }
        public int MemberId { get; }
        public MemberStatus Status { get; }
        public IRaptorController RaptorController { get; }
        public void UpdateMemberStatus();
        public void SetPackManager(IPackManager packManager);

    }
}