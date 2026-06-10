using System.Collections.Generic;
using Infrastructure.Core.Lifecycle.PhysicsEntities;
using UnityEngine;
namespace Infrastructure.Unity.Lifecycle
{
    public class EntityDestroyer : MonoBehaviour
    {
        private Queue<IDestructible> _toDestroy = new();

        public void AddToDestroyQueue(IDestructible destructible)
        {
            _toDestroy.Enqueue(destructible);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _toDestroy.Count; i++)
            {
                var destructible = _toDestroy.Dequeue();
                Debug.Log($"Destroying: {destructible}");
                destructible.Destruct();
            }
        }
    }
}