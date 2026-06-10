using System;
using UnityEngine;

namespace Unity.Common.Unity
{
    [Serializable]
    public class SerializedInterface<T> where T : class
    {
        [SerializeField] private MonoBehaviour _monoScript;

        private T _cached;

        public T Interface
        {
            get
            {
                if (_cached == null && _monoScript != null)
                    _cached = _monoScript as T;

                return _cached;
            }
        }

        public static implicit operator T(SerializedInterface<T> wrapper)
        {
            return wrapper?.Interface;
        }
    }
}