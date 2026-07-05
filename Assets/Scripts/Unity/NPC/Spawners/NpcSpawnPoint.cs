using Core.NPC;
using Infrastructure.Unity.Registries;
using UnityEngine;

namespace Unity.NPC.Spawners
{
    public class NpcSpawnPoint : SelfRegister<INpcSpawnPoint>, INpcSpawnPoint
    {
        public Vector2 Position => transform.position;
    }
}