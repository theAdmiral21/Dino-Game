using UnityEngine;

namespace Unity.Common
{
    public static class ProviderLookUp
    {
        public static T Require<T>(Component from) where T : class
        {
            var found = from.GetComponentInParent<T>();
            if (found == null)
            {
                Debug.LogError($"{from.name}: could not find {typeof(T).Name} in parent");
            }
            return found;
        }
    }
}