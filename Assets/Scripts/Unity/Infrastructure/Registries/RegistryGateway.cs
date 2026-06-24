using System.Collections.Generic;
using UnityEngine;
using Infrastructure.Unity.Registries;
using System;
using Infrastructure.Core.Registries;

namespace Infrastructure.Unity
{
    public static class RegistryGateway
    {

        private static readonly Dictionary<Type, IClearRegistry> _registries = new();
        private static readonly Dictionary<Type, List<object>> _pendingRegistrations = new();
        private static readonly Dictionary<Type, List<object>> _pendingDeregistrations = new();
        private static bool _debug = false;
        public static void SetRegistry<T>(Registry<T> registry)
        {
            var type = typeof(T);
            if (_debug)
                Debug.Log($"SetRegistry called for type: {type.Name} - Frame: {Time.frameCount}");
            _registries[typeof(T)] = registry;
            // Flush pending registrations
            if (_pendingRegistrations.TryGetValue(type, out var pending))
            {
                if (_debug)
                    Debug.Log($"Flushing {pending.Count} pending registrations for {type.Name}");
                foreach (var obj in pending)
                {
                    registry.Register((T)obj);
                }
                pending.Clear();
            }

            // Flush pending deregistrations
            if (_pendingDeregistrations.TryGetValue(type, out var pendingRemoval))
            {
                foreach (var obj in pendingRemoval)
                {
                    registry.Deregister((T)obj);
                }
                pendingRemoval.Clear();
            }

        }

        public static Registry<T> GetRegistry<T>()
        {
            return (Registry<T>)_registries[typeof(T)];
        }


        public static void Register<T>(T entity)
        {
            var type = typeof(T);
            // If we have the registry already, add the new entity
            if (_registries.TryGetValue(type, out var registryObj))
            {
                if (_debug)
                    Debug.Log($"Registering {entity} directly into registry - Frame: {Time.frameCount}");

                ((Registry<T>)registryObj).Register(entity);
            }
            else
            {
                if (_debug)
                    Debug.Log($"No registry for {type.Name} yet, adding {entity} to pending - Frame: {Time.frameCount}");

                // If we don't have a registry for this entity
                if (!_pendingRegistrations.TryGetValue(type, out var tempList))
                {
                    // Make a holding list
                    tempList = new List<object>();
                    _pendingRegistrations[type] = tempList;
                }
                // Add the new entity to the list
                tempList.Add(entity);
            }
        }

        public static void Deregister<T>(T entity)
        {
            var type = typeof(T);
            // If we have the registry already, remove the entity
            if (_registries.TryGetValue(type, out var registryObj))
            {
                ((Registry<T>)registryObj).Deregister(entity);
            }
            else
            {
                // If we don't have a registry for this entity
                if (!_pendingDeregistrations.TryGetValue(type, out var tempList))
                {
                    // Make a holding list
                    tempList = new List<object>();
                    _pendingDeregistrations[type] = tempList;
                }
                // Add the new entity to the list
                tempList.Add(entity);
            }
        }

        public static void ClearRegistries()
        {
            if (_debug)
                Debug.Log($"Clearing registries - Frame: {Time.frameCount}");
            foreach (var registry in _registries.Values)
            {
                registry.Clear();
            }
            _pendingRegistrations.Clear();
            _pendingDeregistrations.Clear();
        }

    }
}