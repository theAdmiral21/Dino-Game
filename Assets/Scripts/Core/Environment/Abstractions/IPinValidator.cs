namespace Core.Environment.Abstractions
{
    public interface IPinValidator
    {
        public void GeneratePin(int length);

        public bool IsValid();
    }
}