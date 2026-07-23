using UnityEngine;

namespace Unity.Base
{
    public abstract class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
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
            DontDestroyOnLoad(gameObject);
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