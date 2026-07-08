namespace Core.Environment.Interactions
{
    public interface IDoor
    {
        public bool IsOpen { get; }
        public bool IsLocked { get; }
        public void SetLocked(bool val);
    }
}