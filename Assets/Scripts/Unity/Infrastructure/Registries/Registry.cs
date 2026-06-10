using System.Collections.Generic;
using Infrastructure.Core.Registries;
using UnityEngine;

namespace Infrastructure.Unity.Registries
{
    [System.Serializable]
    public class Registry<T> : IClearRegistry
    {
        [SerializeField]
        private List<MonoBehaviour> debugEntities = new();

        public HashSet<T> Entities => _entities;
        private HashSet<T> _entities = new();

        public void Register(T entity)
        {
            _entities.Add(entity);
            RefreshDebug();
        }

        public void Deregister(T entity)
        {
            _entities.Remove(entity);
            RefreshDebug();
        }

        public void Clear()
        {
            _entities.Clear();
            RefreshDebug();
        }

        private void RefreshDebug()
        {
            debugEntities.Clear();

            foreach (var entity in _entities)
            {
                if (entity is MonoBehaviour mb)
                    debugEntities.Add(mb);
            }
        }
    }
}