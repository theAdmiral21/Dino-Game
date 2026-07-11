using System.Collections.Generic;
using Core.Ai.BlackBoard;
using Core.NPC;
using Core.NPC.Services;
using Game.Core.Execution;
using Game.Core.Scenes;
using Infrastructure.Unity.Registries;
using Primitives.Audio.EntityKeys;
using Primitives.Common.Scenes;
using Unity.Common.Unity;
using UnityEngine;

namespace Unity.NPC.Spawners
{
    public class NpcSpawnDirector : SelfRegister<IInitializable<IGameContext>>, IInitializable<IGameContext>
    {
        [Header("Init Priority")]
        [SerializeField] private int _priority;
        public int Priority => _priority;

        [Header("Raptor Pack Manager")]
        [SerializeField] Transform _packManagerTransform;
        [SerializeField] private SerializedInterface<IPackManager> _packManagerMono;
        private IPackManager _packManager => _packManagerMono.Interface;

        private ISpawnNpcService _spawnNpcService;
        private ISceneEvents _sceneEvents;
        [SerializeField] private SerializedInterface<INpcSpawnPointRegistry> _npcSpawnRegistryMono;
        private INpcSpawnPointRegistry _npcSpawnRegistry => _npcSpawnRegistryMono.Interface;
        public IReadOnlyCollection<INpcSpawnPoint> NpcSpawnPoints => _npcSpawnRegistry.SpawnPoints;

        private void OnDestroy()
        {
            _sceneEvents.SceneChangeComplete -= SpawnDinos;
            base.OnDestroy();
        }
        public void Initialize(IGameContext context)
        {
            _spawnNpcService = context.SpawnNpcService;
            _sceneEvents = context.SceneServices.SceneEvents;

            _sceneEvents.SceneChangeComplete += SpawnDinos;
        }
        public void PostInitialize(IGameContext context)
        {
            Debug.Assert(_spawnNpcService != null, $"Failed to get SpawnNpcService from game context");
        }
        private void SpawnDinos(SceneId id)
        {
            foreach (var spawnPoint in NpcSpawnPoints)
            {
                // spawn a dino!
                var npc = _spawnNpcService.RequestNpcSpawn(EntityKey.Raptor, spawnPoint.Position);
                // for now we only spawn raptors, so make sure they have a pack
                IPackMember packMember = npc.GetComponent<IPackMember>();
                _packManager.AddMember(packMember);
                npc.transform.SetParent(_packManagerTransform);
            }
        }
    }
}