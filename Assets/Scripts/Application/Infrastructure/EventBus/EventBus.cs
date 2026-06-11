using System;
using System.Collections.Generic;
using UnityEngine;
using Primitives.EventBus.Abstractions;

namespace Infrastructure.Application.EventBus
{
    public class EventBus : IEventBus
    {
        public Dictionary<Type, List<Delegate>> Subscribers => _subscribers;
        private Dictionary<Type, List<Delegate>> _subscribers = new();

        private bool _debugEvents = false;

        public void Publish<T>(T eventData)
        {
            var eventType = typeof(T);

            if (!_subscribers.TryGetValue(eventType, out var delegates))
            {
                Debug.LogError($"No subscribers found for {eventType}");
                return;
            }
            foreach (var handler in delegates)
            {
                ((Action<T>)handler).Invoke(eventData);
                if (_debugEvents) Debug.Log($"Raising event {eventData} for {handler}");
            }
        }

        public void Subscribe<T>(Action<T> handler)
        {
            var handlerType = typeof(T);

            if (!_subscribers.TryGetValue(handlerType, out var delegates))
            {
                delegates = new List<Delegate>();
                _subscribers[handlerType] = delegates;
            }
            if (_debugEvents) Debug.Log($"Subscribed {handler} for {handlerType}");
            delegates.Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var handlerType = typeof(T);

            if (_subscribers.TryGetValue(handlerType, out var delegates))
            {
                delegates.Remove(handler);
                if (_debugEvents) Debug.Log($"Unsubscribed {handler} for {handlerType}");
            }
        }
    }
}