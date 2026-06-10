using System;
using UnityEngine;

namespace Unity.Common.Unity
{
    public static class MonoBehaviourExtensions
    {
        public static T AsInterface<T>(this MonoBehaviour monoScript) where T : class
        {
            if (monoScript is not T result)
            {
                Debug.LogError($"Unable to convert {monoScript} to {typeof(T).Name}.");
                throw new InvalidCastException();
            }
            return result;
        }
    }
}