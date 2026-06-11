namespace Primitives.Input.Abstractions
{
    public interface IInputContext
    {
        public InputContext Type { get; }
        public void Activate();
        public void Deactivate();
    }
}