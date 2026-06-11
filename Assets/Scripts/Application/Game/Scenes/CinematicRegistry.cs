using UnityEngine;
using System.Collections.Generic;
using Game.Core.Cinematics.Abstractions;
using Game.Core.Cinematics.Enums;

namespace Game.Application.Scenes
{
    public static class CinematicRegistry
    {
        public static Dictionary<CinematicId, ICinematicService> Cinematics => _cinematics;
        private static readonly Dictionary<CinematicId, ICinematicService> _cinematics = new();

        public static void Register(ICinematicService service)
        {
            _cinematics[service.Id] = service;
            Debug.Log($"Registerd cinematic: {service.Id}");
        }

        public static void Unregister(ICinematicService service)
        {
            if (_cinematics.TryGetValue(service.Id, out var existing) && existing == service)
            {
                _cinematics.Remove(service.Id);
            }
        }

        public static bool TryGet(CinematicId id, out ICinematicService cinematic)
        {
            if (_cinematics.TryGetValue(id, out cinematic))
            {
                if (cinematic == null)
                {
                    _cinematics.Remove(id);
                    cinematic = null;
                    return false;
                }

                return true;
            }

            return false;
        }

        public static void Clear()
        {
            Debug.Log($"Cinematics cleared");
            _cinematics.Clear();
        }
    }
}