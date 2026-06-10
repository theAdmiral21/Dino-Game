namespace Movement.Core.Rules
{
    public static class RuleStateExtensions
    {
        public static bool TryGet<T>(this object state, out T result) where T : class
        {
            result = state as T;
            return result != null;
        }
    }
}