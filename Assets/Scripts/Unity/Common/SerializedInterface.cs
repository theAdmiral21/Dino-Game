using System;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Unity.Common.Unity
{
    [Serializable]
    public class SerializedInterface<T> where T : class
    {
        // Store as a unity object so we can accept scriptable objects AND monobehaviours
        public UnityEngine.Object Target => _target;
        [SerializeField] private UnityEngine.Object _target;

        private T _cached;

        public T Interface
        {
            get
            {
                if (_cached == null && _target != null)
                    _cached = _target as T;

                return _cached;
            }
        }

        public static implicit operator T(SerializedInterface<T> wrapper)
        {
            return wrapper?.Interface;
        }

        // WAIT BETTER YET I SHOULD JUST DESTROY THIS
        public void Destroy()
        {
            if (_target == null) return;

            if (_target is Component c)
            {
                GameObject.Destroy(c.gameObject);
            }
            else
            {
                GameObject.Destroy(_target);
            }
        }

        public void OnValidate()
        {
            if (_target != null && !(_target is T))
            {
                Debug.LogError($"Assigned object '{_target.name} does not implement {typeof(T).Name}.");
                _target = null;
            }
        }
    }
}