namespace Core.Ai.BlackBoard
{
    public interface IPackMember
    {
        public int MemberId { get; }
        public MemberStatus Status { get; }
        public IRaptorController RaptorController { get; }
        public void UpdateMemberStatus();

    }
}