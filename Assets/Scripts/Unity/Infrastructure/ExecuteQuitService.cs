using Primitives.Common.Infrastructure;

namespace Infrastructure.Application.state.Services
{
    public class ExecuteQuitService : IQuitExecutor
    {
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}