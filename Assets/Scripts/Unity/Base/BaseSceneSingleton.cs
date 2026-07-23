using UnityEngine;

namespace Unity.Base
{
    public abstract class BaseSceneSingleton<T> : MonoBehaviour where T : BaseSceneSingleton<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = (T)this;
            ChildAwake();
            Debug.Log($"INSTANTIATED {typeof(T).Name}");
        }
        private void OnDestroy()
        {
            Debug.Log($"Destroyed {typeof(T).Name}");
            if (Instance == this)
            {
                Instance = null;
            }
            ChildDestroy();
        }

        protected abstract void ChildAwake();
        protected abstract void ChildDestroy();
    }
}