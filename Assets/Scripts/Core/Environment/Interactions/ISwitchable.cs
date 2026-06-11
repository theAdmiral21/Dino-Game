namespace Environment.Core.Interactions
{
    public interface ISwitchable
    {
        public bool IsActive { get; }

        public void Switch();
    }
}