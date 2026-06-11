using System;
using System.Collections.Generic;
using Infrastructure.Core.Services;
using Primitives.Players;
using UnityEngine;

namespace Infrastructure.Application.Services
{
    public class SceneContextService : ISceneContextService
    {

        private Dictionary<IPlayerInfo, Vector2> _positionMap = new();

        public void Clear()
        {

        }



        public Vector2? ConsumeOverworldPosition(IPlayerInfo player)
        {
            if (_positionMap.TryGetValue(player, out Vector2 pos))
            {
                _positionMap.Remove(player);
                return pos;
            }
            // throw new KeyNotFoundException($"Key {player} not found in position map");
            return null;
        }
        public void SetOverworldPosition(IPlayerInfo player, Vector2 pos)
        {
            if (_positionMap.TryAdd(player, pos))
            {
                return;
            }
            throw new ArgumentException($"Key {player} has already been added to position map");
        }

        public bool TryGetContext<T>(out T context) where T : class
        {
            context = this as T;
            return context != null;
        }
    }
}