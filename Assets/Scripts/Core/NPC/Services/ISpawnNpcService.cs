using Primitives.Audio.EntityKeys;
using UnityEngine;

namespace Core.NPC.Services
{
    public interface ISpawnNpcService
    {
        public GameObject RequestNpcSpawn(EnemyEntityKey npcKey, Vector3 location, Quaternion rotation = default);
    }
}